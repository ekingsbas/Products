using Products.Business.Contracts;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;

namespace Products.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IQueryHandler<GetProductByIdQuery, ProductModel> _getProductByIdQueryHandler;
        private readonly ICommandHandler<CreateProductCommand> _createProductCommandHandler;
        private readonly ICommandHandler<UpdateProductCommand> _updateProductCommandHandler;
        private readonly ICacheService _cacheService; 
        private readonly IExternalDiscountService _externalDiscountService;

        public ProductService(
            IQueryHandler<GetProductByIdQuery, ProductModel> getProductByIdQueryHandler,
            ICommandHandler<CreateProductCommand> createProductCommandHandler,
            ICommandHandler<UpdateProductCommand> updateProductCommandHandler,
            ICacheService cacheService,
            IExternalDiscountService externalDiscountService)
        {
            _getProductByIdQueryHandler = getProductByIdQueryHandler;
            _createProductCommandHandler = createProductCommandHandler;
            _updateProductCommandHandler = updateProductCommandHandler;
            _cacheService = cacheService;
            _externalDiscountService = externalDiscountService;
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _getProductByIdQueryHandler.HandleAsync(query);

            if (product == null)
            {
                return null;
            }

            var statusName = _cacheService.GetStatusName(product.Status);

            int discount = await _externalDiscountService.GetDiscountAsync();

            var finalPrice = product.Price * (100 - discount) / 100;

            return new ProductModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                StatusName = statusName,
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                Discount = discount,
                FinalPrice = finalPrice
            };
        }

        public async Task CreateProductAsync(CreateProductCommand command)
        {
            await _createProductCommandHandler.HandleAsync(command);
        }

        public async Task UpdateProductAsync(UpdateProductCommand command)
        {
            await _updateProductCommandHandler.HandleAsync(command);
        }



    }
}
