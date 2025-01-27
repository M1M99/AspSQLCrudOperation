using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ThirdAspSqlCrud.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,100000)]
        public decimal Price { get; set; }
        [Range(1,1000)]
        public decimal Discount { get; set; }
        public string? ImgLink { get; set; }
    }
}