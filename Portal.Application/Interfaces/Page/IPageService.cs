using Common.Common.ActionResponse;
using Common.Dtos.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.Page
{
  public interface IPageService
  {
    Task<ActionResponse<List<PageDto>>> CreateListPage(List<CreateUpdatePage> pages);
  }
}
