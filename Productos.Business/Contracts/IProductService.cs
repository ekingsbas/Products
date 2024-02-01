using Products.BusinessModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Business.Contracts
{
    public interface IProductService : IBaseService<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetProductsByNameAsync(string name);
    }
}
