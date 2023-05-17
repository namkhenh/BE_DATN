using Common.Common.ActionResponse;
using Common.Dtos.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.Menu
{
  public interface IMenuService
  {
    Task<ActionResponse<List<MenuDto>>> CreateList(List<CreateUpdateMenu> menus);
  }
}
