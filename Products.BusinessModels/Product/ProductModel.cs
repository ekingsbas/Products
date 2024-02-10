using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.BusinessModels.Product
{
    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Stock { get; set; } = 0;
        public double Price { get; set; } = 0;
        public bool Status { get; set; } = true;
        public string? StatusName { get; set; }
        public double Discount { get; set; }
        public double FinalPrice { get; set; } 
    }
}
