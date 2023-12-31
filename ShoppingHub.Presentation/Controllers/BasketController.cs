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

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetBasketsByUserId()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("SessionBasketId");
                var query = new GetBasketsByUserIdQuery(userId ?? 0);
                return Ok(await _mediator.Send(query));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{basketId}/orderTotal")]
        public async Task<IActionResult> GetCurrentBasketTotalPrice()
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                var query = new GetCurrentBasketTotalPriceCommand(basketId ?? 0);
                return Ok(await _mediator.Send(query));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("SessionUserId");
                var createBasketCommand = new CreateBasketCommand(userId ?? 0);
                var basketId = await _mediator.Send(createBasketCommand);

                HttpContext.Session.SetInt32("SessionBasketId", basketId);
                return Ok(new { BasketId = basketId });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                //throw;
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{basketId}")]
        public async Task<IActionResult> UpdateBasket(int basketId, [FromBody] UpdateBasketCommand updateBasketCommand)
        {
            try
            {
                if (basketId != updateBasketCommand.BasketId)
                    return BadRequest("Mismatched basket ID in the request.");

                var updatedBasket = await _mediator.Send(updateBasketCommand);
                return Ok(updatedBasket);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}

