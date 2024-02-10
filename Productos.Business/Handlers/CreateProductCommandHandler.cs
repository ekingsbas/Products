using Products.Business.Contracts;
using Products.BusinessModels.Commands;
using Products.Data.Contracts;
using Products.Entities;

namespace Products.Business.Handlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(CreateProductCommand command)
        {
            var product = new ProductEntity
            {
                Name = command.Name,
                Price = command.Price,
                Description = command.Description,
                Status = command.Status,
                Stock = command.Stock,
            };

            await _productRepository.AddAsync(product);
        }
    }
}
