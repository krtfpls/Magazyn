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
    public async Task<IActionResult> Create(CategoryDto category)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Category = category}));
    }
    }
}