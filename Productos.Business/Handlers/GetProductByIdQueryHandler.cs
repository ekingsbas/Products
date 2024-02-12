using Products.Business.Contracts;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;
using Products.Data.Contracts;
using Products.Entities;

namespace Products.Business.Handlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> HandleAsync(GetProductByIdQuery query)
        {
            var product = await _productRepository.GetByIdAsync(query.Id);

            if (product == null)
                return null;

            return new ProductModel
            {
                ProductId = product.ProductId,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Status = product.Status,
                Stock = product.Stock,
            };
        }
    }
}
