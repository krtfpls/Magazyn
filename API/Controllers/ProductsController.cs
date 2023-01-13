using Application.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class ProductsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param, stock = false }));
    }

    [HttpGet("Stock")]
    public async Task<IActionResult> GetStock([FromQuery] ProductParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param, stock = true }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { id = id }));
    }

    [HttpPut()]
    public async Task<IActionResult> Edit(ProductDto item)
    {
        return HandleResult(await Mediator.Send(new Edit.Command { Product = item }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto item)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Product = item }));
    }
}
