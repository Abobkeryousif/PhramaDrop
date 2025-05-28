using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Core.Entities
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal OldPrice { get; set; } 
        public decimal NewPrice { get; set; } 
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate => DateTime.UtcNow.AddYears(2);
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Photo> photos { get; set; }

        public QRcode QRcode { get; set; }
    }
}
