using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Outh2
{
  public class TokenResponse
  {
    public string AccessToken { get; set; }
    public string RefeshToken { get; set; }
  }
}
