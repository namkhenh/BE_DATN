using Common.Contract.Domain.Interfaces;
using Common.Contract.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Portal.Domain.Entities
{
  public class Page : EntityAuditBase<Guid>, IEntityBase<Guid>
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
  }
}
