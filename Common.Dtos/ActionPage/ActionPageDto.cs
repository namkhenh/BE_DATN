using Common.Share.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.ActionPage
{
    public class ActionPageDto: DataEntity
    {
        public Dictionary<string, List<string>> ActionPages { get; set; }
    }
}
