using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using NadinSoft.Application.Features.Products.Commands.DeleteProduct;
using NadinSoft.Application.Features.Products.Commands.UpdateProduct;
using NadinSoft.Application.Features.Products.Queries.GetProducts;
using NadinSoft.Presentation.Helpers;
using System.Net;

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
            var result = await _sender.Send(new GetProductListQuery(nameFilter, manufactureEmailFilter, phoneFilter), cancellationToken);
            return Ok(new 
            { 
                TotalCount = result.Count(), 
                Items = result 
            });
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var result =  await _sender.Send(command, cancellationToken);
            if(result.IsSuccess)
            {
                return Ok(HttpStatusCode.Accepted);
            }

            return BadRequest(result.Errors);
        }
    }
}
