using Common.Common.ActionResponse;
using Common.Dtos.ActionPage;
using Common.Share.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.ActionPage
{
  public interface IActionPageService
  {
    Task<ActionResponse<List<ActionPageDto>>> CreateListActionPage(Dictionary<string, List<string>> request, string menuCode);

  }
}
