using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;

namespace Products.Business.Contracts
{
    public interface IProductService
    {
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task CreateProductAsync(CreateProductCommand command);
    }
}
