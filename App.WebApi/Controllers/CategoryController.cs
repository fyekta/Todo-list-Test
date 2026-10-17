using App.Domain.Core.Contract.Appservice;
using App.Domain.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<CategoryDto> categories = await _categoryAppService.GetAll(cancellationToken);

            return Ok(categories);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            CategoryDto category = await _categoryAppService.GetById(id, cancellationToken);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto category,CancellationToken cancellationToken)
        {
            await _categoryAppService.Add(category, cancellationToken);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CategoryDto category,CancellationToken cancellationToken)
        {
            await _categoryAppService.Update(id, category, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            await _categoryAppService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}