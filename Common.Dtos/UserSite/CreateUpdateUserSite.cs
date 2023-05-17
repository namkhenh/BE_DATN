using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.UserSite
{
  public class CreateUpdateUserSite
  {
    public Guid? Id { get; set; }
    public string Code { get; set; }
    public Guid UserId { get; set; }
    public Guid SiteId { get; set; }
  }
}
