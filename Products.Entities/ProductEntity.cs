using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        public Guid ProductId {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Stock {  get; set; }  = 0;
        public double Price { get; set; } = 0;
        public short Status { get; set; } = 1;
    }
}
