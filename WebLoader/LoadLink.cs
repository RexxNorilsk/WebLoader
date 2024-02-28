using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLoader
{
    public class LoadLink
    {
        public LoadLink(string link, int priority)
        {
            Link = link;
            Priority = priority;
        }

        public int Progress { get; set; }
        public float Speed { get; set; }
        public string Link { get; set; }
        public int Priority { get; set; }
    }
}
