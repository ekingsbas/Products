using Products.Business.Contracts;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;
using Products.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Business.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task HandleAsync(UpdateProductCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId);

            if (product == null)
            {
                throw new Exception($"Product with ID {command.ProductId} not found.");
            }

            // Update properties
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Status = command.Status;
            product.Stock = command.Stock;

            // Save
            await _productRepository.UpdateAsync(product);
        }
    }
}
