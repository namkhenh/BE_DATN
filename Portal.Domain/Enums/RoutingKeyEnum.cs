using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Enums
{
  public class RoutingKeyEnum
  {
    public static GenericEnum SiteAsync = new GenericEnum(id: 1, code: "Site.Sync", name: "Đồng bộ Site");
    public static GenericEnum SiteSend = new GenericEnum(id: 2, code: "Site.Send", name: "Gửi yêu cầu Site");

    public static GenericEnum ProjectAsync = new GenericEnum(id: 1, code: "Project.Sync", name: "Đồng bộ Tenant");
    public static GenericEnum ProjectSend = new GenericEnum(id: 2, code: "Project.Send", name: "Gửi yêu cầu Tenant");

    public static GenericEnum PermissionAsync = new GenericEnum(id: 1, code: "Permission.Sync", name: "Đồng bộ Tenant");
    public static GenericEnum PermissionSend = new GenericEnum(id: 2, code: "Permission.Send", name: "Gửi yêu cầu Tenant");

    public static GenericEnum ActionPageAsync = new GenericEnum(id: 1, code: "ActionPage.Sync", name: "Đồng bộ Tenant");
    public static GenericEnum ActionPageSend = new GenericEnum(id: 2, code: "ActionPage.Send", name: "Gửi yêu cầu Tenant");

    public static GenericEnum ActionAsync = new GenericEnum(id: 1, code: "Action.Sync", name: "Đồng bộ Tenant");
    public static GenericEnum ActionSend = new GenericEnum(id: 2, code: "Action.Send", name: "Gửi yêu cầu Tenant");

  
    public static GenericEnum TenantAsync = new GenericEnum(id: 1, code: "Tenant.Sync", name: "Đồng bộ Tenant");
    public static GenericEnum TenantSend = new GenericEnum(id: 2, code: "Tenant.Send", name: "Gửi yêu cầu Tenant");

    public static GenericEnum ProductAsync = new GenericEnum(id: 1, code: "Product.Sync", name: "Đồng bộ Product");
    public static GenericEnum ProductSend = new GenericEnum(id: 2, code: "Product.Send", name: "Gửi yêu cầu Product");

    public static GenericEnum AppUserAsync = new GenericEnum(id: 1, code: "AppUser.Sync", name: "Đồng bộ AppUser");
    public static GenericEnum AppUserSend = new GenericEnum(id: 1, code: "AppUser.Send", name: "Gửi yêu cầu AppUser");

    public static GenericEnum AppRoleAsync = new GenericEnum(id: 1, code: "AppRole.Sync", name: "Đồng bộ AppUser");
    public static GenericEnum AppRoleSend = new GenericEnum(id: 1, code: "AppRole.Send", name: "Gửi yêu cầu AppRole");
  }
}
