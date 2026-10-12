using App.Domain.Core.Dtos;

namespace App.Domain.Core.Contract.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken);

        Task<CategoryDto?> GetById(int id,CancellationToken cancellationToken);

        Task Add(CategoryDto category,CancellationToken cancellationToken);

        Task Update(int id,CategoryDto category,CancellationToken cancellationToken);

        Task Delete(int id,CancellationToken cancellationToken);
    }
}

