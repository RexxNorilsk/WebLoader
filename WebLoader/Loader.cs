﻿using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebLoader
{
    public class Loader
    {
        private List<ILoadLink> _listToLoadSorted = new List<ILoadLink>();
        private int _loadRemainedByPriority;
        private int _currentPriority = 4;
        private string _loadFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Loads");
        private Action<string> _callback;

        public Loader(ObservableCollection<ILoadLink> loadLinks, Action<string> callback) {
            _callback = callback;
            _listToLoadSorted = loadLinks.OrderBy(t => t.Priority).Reverse().ToList();
            for (int i = 0; i < _listToLoadSorted.Count; i++)
            {
                _listToLoadSorted[i].Progress = 0;
            }
            if (!Directory.Exists(_loadFolder))
            {
                Directory.CreateDirectory(_loadFolder);
            }
            StartToLoadByPriority(_currentPriority);
        }

        public bool IsFinal()
        {
            return _loadRemainedByPriority == 0 && _currentPriority <= 0;
        }

        public void StartToLoadByPriority(int targetPriority)
        {
            _loadRemainedByPriority = 0;
            for (int i = 0; i < _listToLoadSorted.Count; i++)
            {
                if(_listToLoadSorted[i].Priority == targetPriority)
                {
                    _loadRemainedByPriority++;
                    int id = i;
                    string fileNameResult = Path.Combine(_loadFolder, Path.GetFileName(_listToLoadSorted[id].Link));
                    try
                    {
                        using (WebClient wc = new WebClient())
                        {
                            wc.DownloadProgressChanged += (s, e) =>
                            {
                                _listToLoadSorted[id].Progress = e.ProgressPercentage;
                                _listToLoadSorted[id].SetLastBytes(e.BytesReceived);
                            };
                            wc.DownloadFileCompleted += (s, e) =>
                            {
                                _listToLoadSorted[id].CompleteLoad?.Invoke();
                                _loadRemainedByPriority--;
                                _listToLoadSorted[id].Speed = "Завершено";
                                ZipCheck(fileNameResult);
                                CheckNextPriority(null);
                            };

                            wc.DownloadFileAsync(
                                new System.Uri(_listToLoadSorted[id].Link),
                                fileNameResult
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        _loadRemainedByPriority = 0;
                        _currentPriority = 0;
                        CheckNextPriority(ex);
                        return;
                    }
                }
            }
            CheckNextPriority(null);
        }
        private void ZipCheck(string fileName)
        {
            try
            {
                if (ZipFile.IsZipFile(fileName))
                {
                    if (!Directory.Exists(Path.Combine(_loadFolder, Path.GetFileNameWithoutExtension(fileName))))
                    {
                        using (ZipFile zip = ZipFile.Read(fileName))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(_loadFolder, Path.GetFileNameWithoutExtension(fileName)));
                            zip.ExtractAll(di.FullName);
                        }
                    }
                }
            }
            catch (Exception ex){
                _loadRemainedByPriority = 0;
                _currentPriority = 0;
                CheckNextPriority(ex);
            }
        }
        private void CheckNextPriority(Exception ex)
        {
            if (ex != null)
            {
                _callback.Invoke(ex.Message);
            }
            else if(_loadRemainedByPriority == 0)
            {
                if (_currentPriority > 0)
                    StartToLoadByPriority(--_currentPriority);
                else
                    _callback.Invoke("Загрузка завершена");
            }
        }
    }
}
