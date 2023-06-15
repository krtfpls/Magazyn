using Application.Customers;
using Entities.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class CustomerController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CustomerParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query{Params = param}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    [RequestSizeLimit(30_000_000)] 
    public async Task<IActionResult> Create(CustomerDto customer)
    {
        if (Request.ContentLength > 30_000_000)
            return new ObjectResult(new StatusCodeResult(413));
        return HandleResult(await Mediator.Send(new Create.Command { Customer = customer}));
    }

    [HttpPut("{id}")]
    [RequestSizeLimit(30_000_000)] 
    public async Task<IActionResult> Edit(int id, CustomerDto customer)
    {
        if (Request.ContentLength > 30_000_000)
            return new ObjectResult(new StatusCodeResult(413));
        customer.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Customer = customer }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
