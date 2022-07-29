using Application.Documents;
using Application.Documents.DocumentBuilders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DocumentsController: BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DocumentParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpPost("/CreatePZ")]
    public async Task<IActionResult> CreatePZ(DocumentDto document)
    {
        document.Type= "PZ";
        return HandleResult(await Mediator.Send(new Create.Command { Document = document}));
    }

     [HttpPost("/CreateWZ")]
    public async Task<IActionResult> CreateWZ(DocumentDto document)
    {
        document.Type= "WZ";
        return HandleResult(await Mediator.Send(new Create.Command { Document = document}));
    }
}

enum DocumentTypes
{
    PZ,
    WZ
}
