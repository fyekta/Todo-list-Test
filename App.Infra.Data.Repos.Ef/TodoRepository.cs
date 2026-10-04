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

        public async Task<List<TodoItemDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsComplete = x.IsComplete,
                    DueDate = x.DueDate,
                    CreatedAt = x.CreatedAt,
                    CompleteAt = x.CompleteAt
                })
                .ToListAsync(cancellationToken);
        }


        public async Task<TodoItemDto?> GetById(int id,CancellationToken cancellationToken)
        {
            var item = await _context.TodoItems
                .Where(x => x.Id == id)
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsComplete = x.IsComplete,
                    DueDate = x.DueDate,
                    CreatedAt = x.CreatedAt,
                    CompleteAt = x.CompleteAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            return item;
        }


        public async Task Add(TodoItemDto item,CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                Title = item.Title,
                Description = item.Description,
                IsComplete = item.IsComplete,
                DueDate = item.DueDate,
                CreatedAt = item.CreatedAt,
                CompleteAt = item.CompleteAt
            };

            await _context.TodoItems.AddAsync(todoItem,cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (todoItem == null)
                return;

            _context.TodoItems.Remove(todoItem);

            await _context.SaveChangesAsync(cancellationToken);
        }
        

        public async Task Complete(int id,CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (todoItem is null)
                return;

            if (todoItem.IsComplete)
                return;

            todoItem.IsComplete = true;
            todoItem.CompleteAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }

       
        public async Task Uncomplete(int id,CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (todoItem is null)
                return;

            if (!todoItem.IsComplete)
                return;

            todoItem.IsComplete = false;
            todoItem.CompleteAt = null;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
