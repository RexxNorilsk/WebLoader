using System;
using System.Collections.ObjectModel;
using WebLoader;
using Xunit;

namespace Tests
{
    public class LoaderTests
    {
        [Fact]
        public void TestEmptyURL()
        {
            ObservableCollection<LoadLink> loadLinks = new ObservableCollection<LoadLink>();
            loadLinks.Add(new LoadLink("", 0));

            Loader loader = new Loader(
                loadLinks,
                (t) => { Assert.NotEqual(t.Length, 0); }
            );

        }
    }
}
