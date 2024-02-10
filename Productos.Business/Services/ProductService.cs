using Products.Business.Contracts;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;
using Products.Data.Contracts;

namespace Products.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IQueryHandler<GetProductByIdQuery, ProductModel> _getProductByIdQueryHandler;
        private readonly ICommandHandler<CreateProductCommand> _createProductCommandHandler;

        public ProductService(
            IQueryHandler<GetProductByIdQuery, ProductModel> getProductByIdQueryHandler,
            ICommandHandler<CreateProductCommand> createProductCommandHandler)
        {
            _getProductByIdQueryHandler = getProductByIdQueryHandler;
            _createProductCommandHandler = createProductCommandHandler;
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            return await _getProductByIdQueryHandler.HandleAsync(query);
        }

        public async Task CreateProductAsync(CreateProductCommand command)
        {
            await _createProductCommandHandler.HandleAsync(command);
        }
    }
}
