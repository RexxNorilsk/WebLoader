using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebLoader
{
    public class LoadLinkSimple : ILoadLink
    {
       public LoadLinkSimple(string link, int priority)
        {
            Link = link;
            Priority = priority;
        }

        public float Progress {
            get { return _progress; }
            set { _progress = value; }
        }
        public string Speed {
            get { return _speed; }
            set { _speed = value; }
        }
        
        public string Link { get; set; }
        public int Priority { get; set; }
        public Action CompleteLoad { get; set; }

        private DateTime _lastUpdate;
        private long _lastBytes = 0;
        private float _progress;
        private string _speed;

        public void SetLastBytes(long bytes)
        {
            if (_lastBytes == 0)
            {
                _lastUpdate = DateTime.Now;
                _lastBytes = bytes;
                return;
            }

            var now = DateTime.Now;
            var timeSpan = now - _lastUpdate;
            if (timeSpan.Milliseconds != 0)
            {
                var bytesChange = bytes - _lastBytes;
                var bytesPerMillisecond = (bytesChange / timeSpan.Milliseconds)/1024f;
                Speed = $"Скорость: {bytesPerMillisecond} Mb/s";
            }
            _lastBytes = bytes;
            _lastUpdate = now;
        }

    }
}
