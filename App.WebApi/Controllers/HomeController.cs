using App.Domain.Core.Contract.Appservice;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoAppService _todoAppService;

        public TodoController(ITodoAppService todoAppService)
        {
            _todoAppService = todoAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await _todoAppService.GetAll(cancellationToken);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
        {
            var item = await _todoAppService.GetById(id, cancellationToken);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add( string title , string description, DateTime dueDate,CancellationToken cancellationToken)
        {
            await _todoAppService.Add(title,description,dueDate, cancellationToken);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            await _todoAppService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}
