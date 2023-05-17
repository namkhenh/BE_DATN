using Common.Contract.Domain.Interfaces;
using Common.Contract.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Entities
{
  public class RefeshToken : EntityAuditBase<Guid>, IEntityBase<Guid>
  {
    public string UserName { get; set; }
    public string RefeshTokenName { get; set; }  
    public DateTime ExTime { get; set; }
  }
}
