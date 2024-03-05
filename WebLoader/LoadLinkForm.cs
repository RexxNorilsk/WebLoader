using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebLoader
{
    public class LoadLinkForm : DependencyObject, ILoadLink
    {
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(float), typeof(LoadLinkForm));
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(string), typeof(LoadLinkForm));
        public LoadLinkForm(string link, int priority)
        {
            Link = link;
            Priority = priority;
        }

        public float Progress {
            get { return (float)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, (float)value); }
        }
        public string Speed {
            get { return (string)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, (string)value); }
        }
        public Action CompleteLoad { get; set; }

        public string Link { get; set; }
        public int Priority { get; set; }

        private DateTime _lastUpdate;
        private long _lastBytes = 0;

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
