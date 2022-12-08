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
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, ProductDto item)
    {
        item.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Product = item }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto item)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Product = item }));
    }
}
