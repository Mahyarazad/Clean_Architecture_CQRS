using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using NadinSoft.Application.Features.Products.Commands.UpdateProduct;
using NadinSoft.Application.Features.Products.Queries.GetProducts;
using NadinSoft.Presentation.Helpers;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Created("/", result.Value);
            }

            return BadRequest(ResultErrorParser.ParseResultError(result.Errors));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(ResultErrorParser.ParseResultError(result.Errors));
        }

        [AllowAnonymous]
        [HttpGet("get-product-list")]
        public async Task<IActionResult> GetProductList(string? nameFilter, string? manufactureEmailFilter, string? phoneFilter, CancellationToken cancellationToken)
        {
            var query = new GetProductListQuery(nameFilter, manufactureEmailFilter, phoneFilter);
            var result = await _sender.Send(query, cancellationToken);
            if(result.Any())
            {
                return Ok(new { TotalCount = result.Count(), Items = result });
            }

            return NoContent();
        }
    }
}
