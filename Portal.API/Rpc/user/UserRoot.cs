using Portal.Infrastructure;
using System.ComponentModel;
using System.Reflection;

namespace Portal.API.Rpc.user
{
    [DisplayName("Quản lý người dùng")]
    public class UserRoot : Root
    {
        public const string Parent = Module + "/menu";
        public const string Master = Module + "/menu/menu-master";
        public const string Detail = Module + "/menu/menu-detail/*";
        private const string Default = Rpc + Module + "/menu";
        public const string Get = Default + "/get";
        public const string Count = Default + "/count";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public static Dictionary<string, IEnumerable<string>> Action = new Dictionary<string, IEnumerable<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count,
                Get,

            } },

            { "Thêm", new List<string> {
                Parent,
                Master, Count,
                Get,

            }},

            { "Sửa", new List<string> {
                Parent,
                Master, Count,
                Get,
            }},

             { "Xóa", new List<string> {
                Parent,
                Master, Count,
                Get
             }},

        };

    }
}
