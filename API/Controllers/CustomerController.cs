using Application.Customers;
using Entities.Documents;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CustomerController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerDto customer)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Customer = customer}));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, CustomerDto customer)
    {
        customer.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Customer = customer }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
}
