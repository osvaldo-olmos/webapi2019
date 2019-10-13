using System.Net;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Errors;

namespace TodoApi.Controllers
{
    [Route("/errors")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorController : ControllerBase
    {
        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            Error error = new Error(code, parsedCode.ToString(), "Ha ocurrido un error ");

            return Ok(error);
        }

        // // GET: api/Todo
        // [HttpGet]
        // public async Task<IActionResult> GetTodoItems()
        // {
        //     var todoItems = await _todoRepository.GetAllAsync();
        //     return Ok(todoItems);
        // }
    }
}