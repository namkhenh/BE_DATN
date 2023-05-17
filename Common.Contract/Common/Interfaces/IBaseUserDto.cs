using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contract.Common.Interfaces
{
  public interface IBaseUserDto
  {
    object Id { get; }
    bool IsDefaultId();
  }
}
