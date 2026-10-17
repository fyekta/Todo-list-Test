using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entity;
using App.Infra.Data.Db.SqlServer.Ef.Dbctx;
using Microsoft.EntityFrameworkCore;


namespace App.Infra.Data.Repos.Ef
{

     public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Color = x.Color
                })
                .ToListAsync(cancellationToken);
        }


        public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
        {
            CategoryDto? item = await _context.Categories
                .Where(x => x.Id == id)
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Color = x.Color
                })
                .FirstOrDefaultAsync(cancellationToken);

            return item;
        }


        public async Task Add(CategoryDto item, CancellationToken cancellationToken)
        {
            Category categoryItem = new Category
            {
                Title = item.Title,
                Color = item.Color
            };

            await _context.Categories.AddAsync(categoryItem, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            Category? categoryItem = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (categoryItem is null)
                return;

            _context.Categories.Remove(categoryItem);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Update(int id, CategoryDto item, CancellationToken cancellationToken)
        {
            Category? categoryItem = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (categoryItem is null)
                return;

            categoryItem.Title = item.Title;
            categoryItem.Color = item.Color;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}