using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entity;
using App.Infra.Data.Db.SqlServer.Ef.Dbctx;
using Microsoft.EntityFrameworkCore;


namespace App.Infra.Data.Repos.Ef
{

    //public class CategoryRepository : ICategoryRepository
    //{
    //    private readonly AppDbContext _context;

    //    public CategoryRepository(AppDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
    //    {
    //        return await _context.Categories
    //            .Select(x => new CategoryDto
    //            {
    //                Id = x.Id,
    //                Title = x.Title,
    //                Color = x.Color
    //            })
    //            .ToListAsync(cancellationToken);
    //    }

    //    public async Task<CategoryDto?> GetById(int id,CancellationToken cancellationToken)
    //    {
    //        return await _context.Categories
    //            .Where(x => x.Id == id)
    //            .Select(x => new CategoryDto
    //            {
    //                Id = x.Id,
    //                Title = x.Title,
    //                Color = x.Color
    //            })
    //            .FirstOrDefaultAsync(cancellationToken);
    //    }


    //    public async Task Add(CategoryDto category,CancellationToken cancellationToken)
    //    {
    //        var newCategory = new Category
    //        {
    //            Title = category.Title,
    //            Color = category.Color
    //        };

    //        await _context.Categories.AddAsync(newCategory,cancellationToken);

    //        await _context.SaveChangesAsync(cancellationToken);
    //    }

    //    public async Task Update(int id, CategoryDto category, CancellationToken cancellationToken)
    //    {
    //        var existingCategory = await _context.Categories
    //            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    //        if (existingCategory is null)
    //            return;

    //        existingCategory.Title = category.Title;
    //        existingCategory.Color = category.Color;

    //        await _context.SaveChangesAsync(cancellationToken);
    //    }

    //    public async Task Delete(int id,CancellationToken cancellationToken)
    //    {
    //        var category = await _context.Categories
    //            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

    //        if (category == null)
    //            return;

    //        _context.Categories.Remove(category);

    //        await _context.SaveChangesAsync(cancellationToken);
    //    }
    //}
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
            var item = await _context.Categories
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
            var categoryItem = new Category
            {
                Title = item.Title,
                Color = item.Color
            };

            await _context.Categories.AddAsync(categoryItem, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var categoryItem = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (categoryItem is null)
                return;

            _context.Categories.Remove(categoryItem);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Update(int id, CategoryDto item, CancellationToken cancellationToken)
        {
            var categoryItem = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (categoryItem is null)
                return;

            categoryItem.Title = item.Title;
            categoryItem.Color = item.Color;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}