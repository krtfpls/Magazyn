using Application.Documents;
using Application.Documents.DocumentBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class DocumentsController: BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DocumentParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { id = id }));
    }


    [HttpPost("GoodsReceiptNote")]
    [RequestSizeLimit(30_000_000)] 
    public async Task<IActionResult> GoodsReceiptNote(DocumentDto document)
    {
        if (Request.ContentLength > 30_000_000)
            return new ObjectResult(new StatusCodeResult(413));
        return HandleResult(await Mediator.Send(new Create.Command { Document = document, builder= new GoodsReceiptNoteBuilder()}));
    }

    [HttpPost("GoodsDispatchNote")]
    [RequestSizeLimit(30_000_000)]
    public async Task<IActionResult> GoodsDispatchNote(DocumentDto document)
    {
        if (Request.ContentLength > 30_000_000)
            return new ObjectResult(new StatusCodeResult(413));
        return HandleResult(await Mediator.Send(new Create.Command { Document = document, builder = new GoodsDispatchNoteBuilder()}));
    }
     
}
