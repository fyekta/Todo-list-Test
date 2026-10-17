using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Dtos;
using App.Domain.Core.Entity;
using App.Infra.Data.Db.SqlServer.Ef.Dbctx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemDto>> GetAll(string? keyWord, bool? isComplete, List<int>? categoryIds, CancellationToken cancellationToken)
        {
            IQueryable<TodoItem> query = _context.TodoItems.AsQueryable();

            if (categoryIds != null && categoryIds.Count > 0)
                query = query.Where(p =>p.CategoryId.HasValue && categoryIds.Contains(p.CategoryId.Value));

            if (isComplete != null)
                query = query.Where(p => p.IsComplete == isComplete);

            if (keyWord != null && keyWord != "") // string.
                query = query.Where(p => p.Title.Contains(keyWord) || p.Description.Contains(keyWord));

            List<TodoItemDto> result = await query
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsComplete = x.IsComplete,
                    DueDate = x.DueDate,
                    CreatedAt = x.CreatedAt,
                    CompleteAt = x.CompleteAt,
                    CategoryId = x.CategoryId
                })
                .ToListAsync(cancellationToken);
            return result;
        }


        public async Task<TodoItemDto?> GetById(int id, CancellationToken cancellationToken)
        {
            TodoItemDto? item = await _context.TodoItems
                .Where(x => x.Id == id)
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsComplete = x.IsComplete,
                    DueDate = x.DueDate,
                    CreatedAt = x.CreatedAt,
                    CompleteAt = x.CompleteAt,
                    CategoryId = x.CategoryId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return item;
        }


        public async Task Add(TodoItemDto item, CancellationToken cancellationToken)
        {
            TodoItem todoItem = new TodoItem
            {
                Title = item.Title,
                Description = item.Description,
                IsComplete = item.IsComplete,
                DueDate = item.DueDate,
                CreatedAt = item.CreatedAt,
                CompleteAt = item.CompleteAt,
                CategoryId = item.CategoryId
            };

            await _context.TodoItems.AddAsync(todoItem, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (todoItem == null)
                return;

            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Complete(int id,CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            todoItem.IsComplete = true;
            todoItem.CompleteAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Uncomplete(int id,CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            todoItem.IsComplete = false;
            todoItem.CompleteAt = null;

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(int id, TodoItemDto item, CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (todoItem is null)
                return;

            todoItem.Title = item.Title;
            todoItem.Description = item.Description;
            todoItem.DueDate = item.DueDate;
            todoItem.CategoryId = item.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
