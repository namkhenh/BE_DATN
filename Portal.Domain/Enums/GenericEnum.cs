using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.Enums
{
  public class GenericEnum
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public GenericEnum(int id, string code, string name)
    {
      Id = id;
      Code = code;
      Name = name;
    }
  }
}
