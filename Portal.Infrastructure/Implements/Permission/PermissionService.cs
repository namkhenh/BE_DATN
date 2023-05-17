using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces.Permission;
using Portal.Application.Repositories.Interfaces.Action;
using Portal.Application.Repositories.Interfaces.ActionPage;
using Portal.Application.Repositories.Interfaces.Menu;
using Portal.Application.Repositories.Interfaces.Page;
using Portal.Application.Repositories.Interfaces.Permission;
using Portal.Application.Repositories.Interfaces.PermissionAction;
using Portal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements.Permission
{
  public class PermissionService : IPermissionService
  {
    private readonly UserManager<Portal.Domain.Entities.AppUser> _userManager;
    private readonly RoleManager<Portal.Domain.Entities.AppRole> _roleManager;
    private readonly IMenuRepository _menuRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionActionRepository _permissionActionRepository;
    private readonly IPageRepository _pageRepository;
    private readonly IActionPageRepository _actionPageRepository;
    private readonly IActionRepository _actionRepository;
    public PermissionService(UserManager<Portal.Domain.Entities.AppUser> userManager, RoleManager<Portal.Domain.Entities.AppRole> roleManager, IMenuRepository menuRepository, IPermissionRepository permissionRepository, IPermissionActionRepository permissionActionRepository, IPageRepository pageRepository, IActionPageRepository actionPageRepository, IActionRepository actionRepository)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _menuRepository = menuRepository;
      _permissionRepository = permissionRepository;
      _permissionActionRepository = permissionActionRepository;
      _pageRepository = pageRepository;
      _actionPageRepository = actionPageRepository;
      _actionRepository = actionRepository;
    }

        public async Task<bool> CheckPermissionByUserIdAndUrl(string userId, string url)
        {
            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser != null)
            {
                if (appUser.UserName == "Admin")
                {
                    return true;
                }
                List<string> lstRoleId = new();
                var roles = await _roleManager.Roles.ToListAsync();
                var listAppUserRole = await _userManager.GetRolesAsync(appUser);
                foreach (var rolename in listAppUserRole)
                {
                    var roleId = roles.Where(x => x.Name == rolename).FirstOrDefault();
                    if (roleId != null)
                    {
                        lstRoleId.Add(roleId.Id);
                    }
                }
                var lstRole = await _roleManager.Roles.ToListAsync();
                var permissions = await _permissionRepository.FindAll().ToListAsync();
                var menus = await _menuRepository.FindAll().ToListAsync();
                var actions = await _actionRepository.FindAll().ToListAsync();
                var permissionActions = await _permissionActionRepository.FindAll().ToListAsync();
                var pages = await _pageRepository.FindAll().ToListAsync();
                var actionPages = await _actionPageRepository.FindAll().ToListAsync();
                if (listAppUserRole != null && listAppUserRole.Count() > 0)
                {
                    // to do
                    var statusAction = (from aurm in lstRoleId
                                        join r in lstRole on aurm equals r.Id
                                        join per in permissions on r.Id equals per.RoleId
                                        join pam in permissionActions on per.Id equals pam.PermissionId
                                        join apm in actionPages on pam.ActionId equals apm.ActionId
                                        join page in pages on apm.PathId equals page.Id
                                        where r.DeleteAt == null && per.DeleteAt == null &&
                                        page.Path.ToLower() == url
                                        select per.Id
                                     ).Distinct().ToList();
                    if (statusAction != null && statusAction.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;

        }
    }
}
