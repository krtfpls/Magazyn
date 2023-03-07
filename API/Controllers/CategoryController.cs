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
    }
}