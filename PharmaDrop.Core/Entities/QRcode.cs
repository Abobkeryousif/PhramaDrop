using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Entities
{
    public class QRcode
    {
        public int Id { get; set; }
        public string ProductQRcode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
