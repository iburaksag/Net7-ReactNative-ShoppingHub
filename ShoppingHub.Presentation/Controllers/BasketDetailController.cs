using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail;
using ShoppingHub.Application.BasketDetails.Commands.DeleteBasketDetail;
using ShoppingHub.Application.BasketDetails.Queries.GetCurrentBasketDetails;

namespace ShoppingHub.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/basketDetail")]
    public class BasketDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketDetailController> _logger;

        public BasketDetailController(IMediator mediator, ILogger<BasketDetailController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //PRODUCTS IN BASKET
        [HttpGet("list")]
        public async Task<IActionResult> GetCurrentBasketDetails()
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                var query = new GetCurrentBasketDetailsQuery(basketId ?? 0);
                var result = await _mediator.Send(query);

                _logger.LogInformation("GetCurrentBasketDetails successful.");
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in GetCurrentBasketDetails: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCurrentBasketDetails: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //ASLINDA BU ADDING PRODUCT TO BASKET
        [HttpPost]
        public async Task<IActionResult> CreateBasketDetail([FromBody] CreateBasketDetailCommand createBasketDetailCommand)
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                createBasketDetailCommand = createBasketDetailCommand with { BasketId = basketId ?? 0 };
                var result = await _mediator.Send(createBasketDetailCommand);

                _logger.LogInformation("CreateBasketDetail successful.");
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in CreateBasketDetail: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateBasketDetail: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //REMOVE PRODUCT FROM BASKET
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBasketDetail(int basketDetailId)
        {
            try
            {
                var command = new DeleteBasketDetailCommand(basketDetailId);
                var isDeleted = await _mediator.Send(command);

                if (isDeleted)
                {
                    _logger.LogInformation("DeleteBasketDetail successful. BasketDetailId: {BasketDetailId}", basketDetailId);
                    return Ok(new { Message = "BasketDetail successfully removed." });
                }
                else
                {
                    _logger.LogWarning("DeleteBasketDetail failed. BasketDetailId: {BasketDetailId}", basketDetailId);
                    return NotFound(new { Message = "BasketDetail not found or unable to remove." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteBasketDetail: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}

