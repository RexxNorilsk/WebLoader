using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLoader
{
    public interface ILoadLink
    {
        float Progress { get; set; }
        string Speed { get; set; }
        string Link { get; set; }
        int Priority { get; set; }
        Action CompleteLoad { get; set; }
        void SetLastBytes(long bytes);
    }
}
