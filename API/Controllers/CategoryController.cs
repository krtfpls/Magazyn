using Application.Categories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}