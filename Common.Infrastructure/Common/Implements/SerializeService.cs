using Common.Contract.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Infrastructure.Common.Implements
{
  public class SerializeService : ISerializeService
  {
    public string Serialize<T>(T obj)
    {
      return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
      {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Converters = new List<JsonConverter>
            {
                new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            }
      });
    }

    public string Serialize<T>(T obj, Type type)
    {
      return JsonConvert.SerializeObject(obj, type, new JsonSerializerSettings());
    }

    public T Deserialize<T>(string text)
    {
      var content = JsonConvert.DeserializeObject<T>(text);
      return JsonConvert.DeserializeObject<T>(text);
    }
  }
}
