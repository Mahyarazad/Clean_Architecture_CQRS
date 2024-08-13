using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using NadinSoft.Application.Features.Products.Commands.UpdateProduct;
using NadinSoft.Presentation.Helpers;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
   
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Created("/", result.Value);
            }
            else
            {
                return BadRequest(ResultErrorParser.ParseResultError(result.Errors));
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(ResultErrorParser.ParseResultError(result.Errors));
            }
        }
    }
}
