using Moq;
using Products.Business.Contracts;
using Products.Business.Services;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;
using Products.BusinessModels.Queries;

namespace Products.Business.Test
{
    [TestFixture]
    public class ProductServiceTests
    {
        [Test]
        public async Task GetProductByIdAsync_ReturnsProduct_WhenFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new ProductModel
            {
                ProductId = productId,
                Name = "Test Product",
                Status = 1,
                Stock = 10,
                Description = "Test Description",
                Price = 100
            };

            var queryHandlerMock = new Mock<IQueryHandler<GetProductByIdQuery, ProductModel>>();
            queryHandlerMock.Setup(handler => handler.HandleAsync(It.IsAny<GetProductByIdQuery>()))
                            .ReturnsAsync(expectedProduct);

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(service => service.GetStatusName(It.IsAny<short>()))
                            .Returns("Active");

            var externalDiscountServiceMock = new Mock<IExternalDiscountService>();
            externalDiscountServiceMock.Setup(service => service.GetDiscountAsync())
                                       .Returns(Task.FromResult((short)10)); // Simulate a discount value

            var productService = new ProductService(queryHandlerMock.Object, null, null, cacheServiceMock.Object, externalDiscountServiceMock.Object);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedProduct.ProductId, result.ProductId);
            Assert.AreEqual(expectedProduct.Name, result.Name);
            Assert.AreEqual(expectedProduct.Status, result.Status);
            Assert.AreEqual(expectedProduct.Stock, result.Stock);
            Assert.AreEqual(expectedProduct.Description, result.Description);
            Assert.AreEqual(expectedProduct.Price, result.Price);
           
        }

        [Test]
        public async Task GetProductByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var queryHandlerMock = new Mock<IQueryHandler<GetProductByIdQuery, ProductModel>>();
            queryHandlerMock.Setup(handler => handler.HandleAsync(It.IsAny<GetProductByIdQuery>()))
                            .ReturnsAsync((ProductModel)null);

            var cacheServiceMock = new Mock<ICacheService>();

            var externalDiscountServiceMock = new Mock<IExternalDiscountService>();

            var productService = new ProductService(queryHandlerMock.Object, null, null, cacheServiceMock.Object, externalDiscountServiceMock.Object);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetProductByIdAsync_ReturnsProductWithFinalPrice_WhenFoundAndDiscountIsAvailable()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new ProductModel
            {
                ProductId = productId,
                Name = "Test Product",
                Status = 1,
                Stock = 10,
                Description = "Test Description",
                Price = 100
            };

            var queryHandlerMock = new Mock<IQueryHandler<GetProductByIdQuery, ProductModel>>();
            queryHandlerMock.Setup(handler => handler.HandleAsync(It.IsAny<GetProductByIdQuery>()))
                            .ReturnsAsync(expectedProduct);

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(service => service.GetStatusName(It.IsAny<short>()))
                            .Returns("Active");

            var externalDiscountServiceMock = new Mock<IExternalDiscountService>();
            externalDiscountServiceMock.Setup(service => service.GetDiscountAsync())
                                       .Returns(Task.FromResult((short)20)); // Simulate a discount value

            var productService = new ProductService(queryHandlerMock.Object, null, null, cacheServiceMock.Object, externalDiscountServiceMock.Object);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Greater(result.FinalPrice, 0);
        }

        
        [Test]
        public async Task CreateProductAsync_CallsCreateCommandHandler_WithCorrectCommand()
        {
            // Arrange
            var createCommand = new CreateProductCommand 
            (
                name: "Test Product",
                status: 1,
                stock: 10,
                description: "Test Description",
                price: 100
            );

            var createCommandHandlerMock = new Mock<ICommandHandler<CreateProductCommand>>();

            var productService = new ProductService(null, createCommandHandlerMock.Object, null, null, null);

            // Act
            await productService.CreateProductAsync(createCommand);

            // Assert
            createCommandHandlerMock.Verify(handler => handler.HandleAsync(createCommand), Times.Once);
        }

        [Test]
        public async Task UpdateProductAsync_CallsUpdateCommandHandler_WithCorrectCommand()
        {
            // Arrange
            var updateCommand = new UpdateProductCommand
            {
                Name = "Test Product",
                Status = 1,
                Stock = 20,
                Description = "Test Description 2",
                Price = 200
            };
            

            var updateCommandHandlerMock = new Mock<ICommandHandler<UpdateProductCommand>>();

            var productService = new ProductService(null, null, updateCommandHandlerMock.Object, null, null);

            // Act
            await productService.UpdateProductAsync(updateCommand);

            // Assert
            updateCommandHandlerMock.Verify(handler => handler.HandleAsync(updateCommand), Times.Once);
        }
    }
}
