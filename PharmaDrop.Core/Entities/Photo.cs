using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }
}
