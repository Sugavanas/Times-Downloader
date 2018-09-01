using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMES_Downloader
{
    class DownloadItem
    {
        public string name { get; set; }
        public string folder { get; set; }
        public string url { get; set; }

        public DownloadItem(string name, string folder, string url)
        {
            this.name = name;
            this.folder = folder;
            this.url = url;
        }
       
    }
}
