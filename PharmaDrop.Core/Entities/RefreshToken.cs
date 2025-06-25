using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpierOn { get; set; }
        public bool IsExiperd => DateTime.Now >= ExpierOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokeOn { get; set; }
        public bool IsActive => RevokeOn == null && !IsExiperd;
        public int userId { get; set; }
        public User User { get; set; }
    }
}
