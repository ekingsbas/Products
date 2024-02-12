using Microsoft.AspNetCore.Mvc;
using Products.Business.Contracts;
using Products.BusinessModels.Commands;
using Products.BusinessModels.Product;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductModel), 200)] 
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return StatusCode(200, product);            
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching product: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _productService.CreateProductAsync(command);
                
                return StatusCode(201, "Product successfully created!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating product: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            try
            {
                if (id != command.ProductId)
                {
                    return BadRequest("ProductId in URL does not match ProductId in request body");
                }

                await _productService.UpdateProductAsync(command);

                return StatusCode(204, "Product successfully created!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating product: {ex.Message}");
            }
        }

    }
}
