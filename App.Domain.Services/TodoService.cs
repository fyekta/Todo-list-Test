using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Service;
using App.Domain.Core.Dtos;

namespace App.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


        public async Task<List<TodoItemDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _todoRepository.GetAll(cancellationToken);
        }


        public async Task<TodoItemDto?> GetById(int id,CancellationToken cancellationToken)
        {
            return await _todoRepository.GetById(id,cancellationToken);
        }


        public async Task Add(string title, string description, DateTime dueDate, CancellationToken cancellationToken)
        {
            TodoItemDto dto = new TodoItemDto()
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                CreatedAt = DateTime.Now,
                IsComplete = false,
                CompleteAt = null

            };

            await _todoRepository.Add(dto, cancellationToken);
        }


        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            await _todoRepository.Delete(id,cancellationToken);
        }


        public async Task Complete(int id,CancellationToken cancellationToken)
        {
            await _todoRepository.Complete(id, cancellationToken);
        }

    

        public async Task Uncomplete(int id,CancellationToken cancellationToken)
        {
            await _todoRepository.Uncomplete(id,cancellationToken);
        }
    }
}
