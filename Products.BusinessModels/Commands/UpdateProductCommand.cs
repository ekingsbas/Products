using Products.BusinessModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.BusinessModels.Commands
{
    public class UpdateProductCommand 
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Stock { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        
    }
}
