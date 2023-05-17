using Common.Contract.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contract.Domain
{
  public abstract class EntityBase<TKey> : IEntityBase<TKey>
  {
    public TKey Id { get; set; }
  }
}
