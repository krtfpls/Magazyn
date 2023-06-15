using Application.Categories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query{Params = param}));
        }

        [HttpPost]
        [RequestSizeLimit(30_000_000)]
        public async Task<IActionResult> Create(CategoryDto category)
        {
            if (Request.ContentLength > 30_000_000)
                return new ObjectResult(new StatusCodeResult(413));
            return HandleResult(await Mediator.Send(new Create.Command { Category = category}));
        }
        }
}