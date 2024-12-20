﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Features.Products.Commands.CreateProduct;
using NadinSoft.Application.Features.Products.Commands.DeleteProduct;
using NadinSoft.Application.Features.Products.Commands.UpdateProduct;
using NadinSoft.Application.Features.Products.Queries.GetProducts;
using NadinSoft.Presentation.Helpers;
using System.ComponentModel.DataAnnotations;
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

        [HttpPut("update")]
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
        public async Task<IActionResult> GetProductList(
            [FromQuery, Required] int pageNumber,
            [FromQuery, Required] int pageSize,
            string? nameFilter, string? manufactureEmailFilter
            , string? phoneFilter, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetProductListQuery(pageNumber, pageSize, nameFilter, manufactureEmailFilter, phoneFilter), cancellationToken);
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

            if(result.HasError(x => x.Message == HttpStatusCode.Unauthorized.ToString()))
            {
                return Unauthorized();
            }

            if(result.HasError(x => x.Message == HttpStatusCode.Forbidden.ToString()))
            {
                return Forbid();
            }

            if(result.HasError(x => x.Message == HttpStatusCode.NotFound.ToString()))
            {
                return NotFound();
            }

            return BadRequest(result.Errors);
        }
    }
}
