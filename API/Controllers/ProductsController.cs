using Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class ProductsController: BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]ProductParams param){
            
            return HandlePagedResult(await Mediator.Send(new List.Query{Params= param}));
        }
    }
