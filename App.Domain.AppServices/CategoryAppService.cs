using App.Domain.Core.Contract.Appservice;
using App.Domain.Core.Contract.Service;
using App.Domain.Core.Dtos;

namespace App.Domain.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _categoryService.GetAll(cancellationToken);
        }

        public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
        {
            return await _categoryService.GetById(id, cancellationToken);
        }
      
        public async Task Add(CategoryDto category, CancellationToken cancellationToken)
        {
            await _categoryService.Add(category, cancellationToken);
        }

        public async Task Update(int id, CategoryDto category, CancellationToken cancellationToken)
        {
            await _categoryService.Update(id, category, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Delete(id, cancellationToken);
        }
    }
}