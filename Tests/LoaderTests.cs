using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using WebLoader;
using Xunit;

namespace Tests
{
    public class LoaderTests
    {
        [Fact]
        public void TestEmptyURL()
        {
            ObservableCollection<ILoadLink> loadLinks = new ObservableCollection<ILoadLink>();
            loadLinks.Add(new LoadLinkSimple("", 0));

            Loader loader = new Loader(
                loadLinks,
                (t) => { 
                    Assert.Equal(t, "Invalid URI: The URI is empty."); 
                }
            );
        }

        [Fact]
        public void TestEmptyList()
        {
            ObservableCollection<ILoadLink> loadLinks = new ObservableCollection<ILoadLink>();
            Loader loader = new Loader(
                loadLinks,
                (t) => { 
                    Assert.Equal(t, "Загрузка завершена"); 
                }
            );
        }

        [Fact]
        public void LoadFile()
        {
            string testLink = "https://static.tildacdn.com/tild3234-3139-4632-b865-373038623932/3137.jpg";
            ObservableCollection<ILoadLink> loadLinks = new ObservableCollection<ILoadLink>();
            loadLinks.Add(new LoadLinkSimple(testLink, 0));
            
            Loader loader = new Loader(
                loadLinks,
                (t) => {
                    string loadFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Loads");
                    string fileName = Path.Combine(loadFolder, Path.GetFileName(testLink));
                    bool findFile = File.Exists(fileName);
                    Assert.True(findFile); 
                }
            );
        }

        [Fact]
        public void CheckPriority()
        {
            string testLowLink = "https://static.tildacdn.com/tild3234-3139-4632-b865-373038623932/3137.jpg";
            string testHighLink = "https://gas-kvas.com/grafic/uploads/posts/2023-09/1695932863_gas-kvas-com-p-kartinki-tsveti-krupnie-20.jpg";
            ObservableCollection<ILoadLink> loadLinks = new ObservableCollection<ILoadLink>();
            
            LoadLinkSimple lowPriority = new LoadLinkSimple(testLowLink, 0);
            LoadLinkSimple highPriority = new LoadLinkSimple(testHighLink, 3);
            loadLinks.Add(lowPriority);
            loadLinks.Add(highPriority);

            bool result = false;
            highPriority.CompleteLoad += () =>
            {
                string loadFolder = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Loads");

                string fileNameHigh = Path.Combine(loadFolder, Path.GetFileName(testHighLink));
                bool findFileHigh = File.Exists(fileNameHigh);

                string fileNameLow = Path.Combine(loadFolder, Path.GetFileName(testLowLink));
                bool findFileLow = File.Exists(fileNameLow);

                result = findFileHigh && !findFileLow;
            };

            Loader loader = new Loader(
                loadLinks,
                (t) => {
                    Assert.Equal(true, result);
                }
            );
            System.Threading.Thread.Sleep(20000);
        }
    }
}
