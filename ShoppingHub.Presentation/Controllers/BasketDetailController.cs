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

        public BasketDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCurrentBasketDetails()
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                var query = new GetCurrentBasketDetailsQuery(basketId ?? 0);
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

        //ASLINDA BU ADDING PRODUCT TO BASKET
        [HttpPost]
        public async Task<IActionResult> CreateBasketDetail([FromBody] CreateBasketDetailCommand createBasketDetailCommand)
        {
            try
            {
                var basketId = HttpContext.Session.GetInt32("SessionBasketId");
                createBasketDetailCommand = createBasketDetailCommand with { BasketId = basketId ?? 0 };

                var createdBasketDetail = await _mediator.Send(createBasketDetailCommand);
                return Ok(createdBasketDetail);
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

        //REMOVE PRODUCT FROM BASKET
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBasketDetail(int basketDetailId)
        {
            try
            {
                var command = new DeleteBasketDetailCommand(basketDetailId);
                var isDeleted = await _mediator.Send(command);

                if (isDeleted)
                    return Ok(new { Message = "BasketDetail successfully removed." });
                else
                    return NotFound(new { Message = "BasketDetail not found or unable to remove." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}

