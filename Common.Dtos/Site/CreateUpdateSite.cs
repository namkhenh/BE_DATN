using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Site
{
    public class CreateUpdateSite
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
