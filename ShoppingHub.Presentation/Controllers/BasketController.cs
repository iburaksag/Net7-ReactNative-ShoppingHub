using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Application.Baskets.Commands.CreateBasket;
using ShoppingHub.Application.Baskets.Commands.UpdateBasket;
using ShoppingHub.Application.Baskets.Queries.GetBasketsByUserId;
using ShoppingHub.Application.Baskets.Queries.GetBasketTotalPrice;

namespace ShoppingHub.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBasketsByUserId()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("SessionUserId");
                var query = new GetBasketsByUserIdQuery(userId ?? 0);
                var result = await _mediator.Send(query);

                _logger.LogInformation($"GetBasketsByUserId for User {userId} successful.");
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in getting user's basket list {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting user's basket list  {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("orderTotal")]
        public async Task<IActionResult> GetCurrentBasketTotalPrice()
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                var query = new GetCurrentBasketTotalPriceCommand(basketId ?? 0);
                var result = await _mediator.Send(query);

                _logger.LogInformation("Getting current basket total is successful.");
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Error in GetCurrentBasketTotalPrice: {ErrorMessage}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCurrentBasketTotalPrice: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("SessionUserId");
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");

                if (basketId == 0)
                {
                    var createBasketCommand = new CreateBasketCommand(userId ?? 0);
                    basketId = await _mediator.Send(createBasketCommand);
                    HttpContext.Session.SetInt32("SessionBasketId", basketId.Value);
                }

                _logger.LogInformation("CreateBasket successful. BasketId: {BasketId}", basketId);
                return Ok(new { BasketId = basketId });
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in CreateBasket: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateBasket: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("complete")]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketCommand updateBasketCommand)
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                updateBasketCommand = updateBasketCommand with { BasketId = basketId ?? 0 };
                var result = await _mediator.Send(updateBasketCommand);

                if (result != null)
                    HttpContext.Session.SetInt32("SessionBasketId", 0);

                _logger.LogInformation("UpdateBasket successful. BasketId: {BasketId}", basketId);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error in UpdateBasket: {ErrorMessage}", ex.Message);
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateBasket: {ErrorMessage}", ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}

