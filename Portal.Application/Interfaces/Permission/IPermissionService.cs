using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces.Permission
{
  public interface IPermissionService
  {
    Task<bool> CheckPermissionByUserIdAndUrl(string userId, string url);
  }
}
