using Common.Contract.Domain;
using Common.Contract.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Entities
{
  public class Permission : EntityAuditBase<Guid>, IEntityBase<Guid>
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string RoleId { get; set; }
    public Guid MenuId { get; set; }

  }
}
