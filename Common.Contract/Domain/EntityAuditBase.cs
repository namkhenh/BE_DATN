using Common.Contract.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contract.Domain
{
  public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
  {
    public DateTime? DeleteAt { get; set; }
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? LastModifiedDate { get; set; } = DateTimeOffset.Now;
  }
}
