using Products.Business.Contracts;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;
using Products.Data.Contracts;
using Products.Entities;

namespace Products.Business.Handlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductEntity>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductEntity> HandleAsync(GetProductByIdQuery query)
        {
            return await _productRepository.GetByIdAsync(query.Id);
        }
    }
}
