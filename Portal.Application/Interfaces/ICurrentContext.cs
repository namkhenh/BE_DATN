using Common.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interfaces
{
  public interface ICurrentContext : IServiceScoped
  {
    public string Token { get; set; }
  }
}
