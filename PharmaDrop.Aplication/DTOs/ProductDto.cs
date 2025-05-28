using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate => DateTime.UtcNow.AddYears(2);
        public int CategoryId { get; set; }

        public IFormFileCollection Photo {  get; set; }
    }

    public record GetByNameProductDto(string Name);

    public class UpdateProductDto : ProductDto 
    {
        public int Id { get; set; }
    }

    public record ProductDetailsQRcode 
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpiryDate => DateTime.UtcNow.AddYears(2);
     }
         
}
