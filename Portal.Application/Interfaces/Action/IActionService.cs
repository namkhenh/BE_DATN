using Common.Common.ActionResponse;
using Common.Dtos.Action;
using Common.Share.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.Action
{
  public interface IActionService
  {
    Task<ActionResponse<List<ActionDto>>> CreateListAction(List<CreateUpdateAction> actions);
  }
}
