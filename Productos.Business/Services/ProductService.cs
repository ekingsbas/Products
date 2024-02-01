using Products.Business.Contracts;
using Products.BusinessModels.Product;
using Products.Data.Contracts;

namespace Products.Business.Services
{
    public class ProductService : BaseService<ProductModel>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByNameAsync(string name)
        {
            var product = await _productRepository.GetProductsByNameAsync(name);

            var dto 
        }
        
    }

}
