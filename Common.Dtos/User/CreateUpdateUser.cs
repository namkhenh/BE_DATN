using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.User
{
  public class CreateUpdateUser
  {
    public Guid? Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }
    public Guid? SiteId { get; set; }
    public Guid? TenantId { get; set; }
    public string Adress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
  }
}
