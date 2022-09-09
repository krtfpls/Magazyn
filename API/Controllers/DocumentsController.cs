using Application.Documents;
using Application.Documents.DocumentHelpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DocumentsController: BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DocumentParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpPost("CreatePZ")]
    public async Task<IActionResult> CreatePZ(DocumentDto document)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Document = new CreatePZDocument(document)}));
    }

     [HttpPost("CreateWZ")]
    public async Task<IActionResult> CreateWZ(DocumentDto document)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Document = new CreateWZDocument(document)}));
    }
}
