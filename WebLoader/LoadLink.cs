using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebLoader
{
    public class LoadLink : DependencyObject
    {
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(float), typeof(LoadLink));

        public LoadLink(string link, int priority)
        {
            Link = link;
            Priority = priority;
        }

        public float Progress {
            get { return (float)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, (float)value); }
        }
        public string Speed { get; set; }
        public float SpeedCount 
        { 
            get { return SpeedCount; }  
            set
            {
                SpeedCount = value;
                Speed = $"Скорость: {SpeedCount} Mb/s";
            }
        }
        public string Link { get; set; }
        public int Priority { get; set; }
    }
}
