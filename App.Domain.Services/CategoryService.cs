using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Service;
using App.Domain.Core.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace App.Domain.Services
{
    //public class CategoryService : ICategoryService
    //{
    //    private readonly ICategoryRepository _categoryRepository;

    //    public CategoryService(ICategoryRepository categoryRepository)
    //    {
    //        _categoryRepository = categoryRepository;
    //    }

    //    public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
    //    {
    //        return await _categoryRepository.GetAll(cancellationToken);
    //    }

    //    public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
    //    {
    //        var category = await _categoryRepository.GetById(id, cancellationToken);

    //        if (category is null)
    //            throw new KeyNotFoundException("Category not found.");

    //        return category;
    //    }

    //    public async Task Add(CategoryDto category, CancellationToken cancellationToken)
    //    {
    //        if (category is null)
    //            throw new ValidationException("Category is required.");

    //        if (string.IsNullOrWhiteSpace(category.Title))
    //            throw new ValidationException("Title is required.");

    //        if (category.Title.Trim().Length > 100)
    //            throw new ValidationException("Title cannot exceed 100 characters.");

    //        if (string.IsNullOrWhiteSpace(category.Color))
    //            throw new ValidationException("Color is required.");

    //        //if (!Regex.IsMatch(category.Color, "^#[0-9A-Fa-f]{6}$"))
    //        //    throw new ValidationException("Color must be a valid HEX code.");

    //        category.Title = category.Title.Trim();

    //        await _categoryRepository.Add(category, cancellationToken);
    //    }

    //    public async Task Update(int id, CategoryDto category, CancellationToken cancellationToken)
    //    {
    //        var existingCategory = await _categoryRepository.GetById(id, cancellationToken);

    //        if (existingCategory is null)
    //            throw new KeyNotFoundException("Category not found.");

    //        if (category is null)
    //            throw new ValidationException("Category is required.");

    //        if (string.IsNullOrWhiteSpace(category.Title))
    //            throw new ValidationException("Title is required.");

    //        if (category.Title.Trim().Length > 100)
    //            throw new ValidationException("Title cannot exceed 100 characters.");

    //        if (string.IsNullOrWhiteSpace(category.Color))
    //            throw new ValidationException("Color is required.");

    //        //if (!Regex.IsMatch(category.Color, "^#[0-9A-Fa-f]{6}$"))
    //        //    throw new ValidationException("Color must be a valid HEX code.");

    //        category.Title = category.Title.Trim();

    //        await _categoryRepository.Update(id, category, cancellationToken);
    //    }

    //    public async Task Delete(int id, CancellationToken cancellationToken)
    //    {
    //        var existingCategory = await _categoryRepository.GetById(id, cancellationToken);

    //        if (existingCategory is null)
    //            throw new KeyNotFoundException("Category not found.");

    //        await _categoryRepository.Delete(id, cancellationToken);
    //    }
    //}
 
   
     public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<List<CategoryDto>> GetAll(CancellationToken cancellationToken)
            {
                return await _categoryRepository.GetAll(cancellationToken);
            }

            public async Task<CategoryDto> GetById(int id, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetById(id, cancellationToken);

                if (category is null)
                {
                    throw new KeyNotFoundException($"Category with id {id} was not found.");
                }

                return category;
            }

            public async Task Add(CategoryDto category, CancellationToken cancellationToken)
            {
                if (category is null)
                    throw new ValidationException("Category is required.");

                if (string.IsNullOrWhiteSpace(category.Title))
                    throw new ValidationException("Title is required.");

                if (category.Title.Trim().Length < 3 || category.Title.Trim().Length > 100)
                {
                    throw new ValidationException("Title must be between 3 and 100 characters.");
                }

                if (string.IsNullOrWhiteSpace(category.Color))
                    throw new ValidationException("Color is required.");

                if (!Regex.IsMatch(category.Color, "^#[0-9A-Fa-f]{6}$"))
            {
                throw new ValidationException("Color must be a valid HEX code.");
            }

            category.Title = category.Title.Trim();

                await _categoryRepository.Add(category, cancellationToken);
            }

            public async Task Update(int id, CategoryDto category, CancellationToken cancellationToken)
            {
                var categoryItem = await _categoryRepository.GetById(id, cancellationToken);

                if (categoryItem is null)
                {
                    throw new KeyNotFoundException($"Category with id {id} was not found.");
                }

                if (category is null)
                    throw new ValidationException("Category is required.");

                if (string.IsNullOrWhiteSpace(category.Title))
                    throw new ValidationException("Title is required.");

                if (category.Title.Trim().Length < 3 || category.Title.Trim().Length > 100)
                {
                    throw new ValidationException("Title must be between 3 and 100 characters.");
                }

                if (string.IsNullOrWhiteSpace(category.Color))
                    throw new ValidationException("Color is required.");

                if (!Regex.IsMatch(category.Color, "^#[0-9A-Fa-f]{6}$"))
            {
                throw new ValidationException("Color must be a valid HEX code.");
            }

            category.Title = category.Title.Trim();

                await _categoryRepository.Update(id, category, cancellationToken);
            }

            public async Task Delete(int id, CancellationToken cancellationToken)
            {
                CategoryDto? category = await _categoryRepository.GetById(id, cancellationToken);

                if (category is null)
                {
                    throw new KeyNotFoundException($"Category with id {id} was not found.");
                }

                await _categoryRepository.Delete(id, cancellationToken);
            }
        }
    
}