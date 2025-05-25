using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Entities
{
    public class OTP
    {
        public Guid Id { get; set; }
        public string otp { get; set; }
        public string UserEmail { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpirationOn { get; set; }
        public bool IsExpier => DateTime.UtcNow > ExpirationOn;

    }
}
