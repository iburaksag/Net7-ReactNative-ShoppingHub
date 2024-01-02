using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Application.Products.Commands.DeleteProduct;
using ShoppingHub.Application.Products.Commands.UpdateProduct;
using ShoppingHub.Application.Products.Queries.GetAllProducts;
using ShoppingHub.Application.Products.Queries.GetProductById;

namespace ShoppingHub.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query);

                _logger.LogInformation("GetAllProducts successful.");
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in GetAllProducts: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Not Found error in GetAllProducts: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProducts: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _mediator.Send(query);

                _logger.LogInformation("GetProductById successful. ProductId: {ProductId}", id);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in GetProductById: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in GetProductById: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProductById: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            try
            {
                var createdProduct = await _mediator.Send(createProductCommand);
                _logger.LogInformation("CreateProduct successful.");
                return Ok(createdProduct);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in CreateProduct: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateProduct: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand updateProductCommand)
        {
            try
            {
                updateProductCommand.Id = id;
                updateProductCommand.UpdatedAt = DateTime.UtcNow;
                var updatedProduct = await _mediator.Send(updateProductCommand);

                _logger.LogInformation("UpdateProduct successful. ProductId: {ProductId}", id);
                return Ok(updatedProduct);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in UpdateProduct: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in UpdateProduct: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProduct: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));
                _logger.LogInformation("DeleteProduct successful. ProductId: {ProductId}", id);
                return Ok(); 
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in DeleteProduct: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProduct: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}

