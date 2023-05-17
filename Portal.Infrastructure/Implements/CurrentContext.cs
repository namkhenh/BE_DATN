using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Implements
{
  public class CurrentContext : ICurrentContext
  {
    public string Token { get; set; }
  }
}
