using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Site
{
    public class SitePageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
