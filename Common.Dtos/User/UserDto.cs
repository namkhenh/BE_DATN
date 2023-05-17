using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
