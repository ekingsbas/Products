using Products.Entities;

namespace Products.Data.Contracts
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetProductsByNameAsync(string name);
    }
}
