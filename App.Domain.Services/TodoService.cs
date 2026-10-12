using App.Domain.Core.Contract.Repository;
using App.Domain.Core.Contract.Service;
using App.Domain.Core.Dtos;
using System.ComponentModel.DataAnnotations;

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
            if (string.IsNullOrWhiteSpace(title))
                throw new ValidationException(
                    "Title is required.");

            if (title.Trim().Length < 3 || title.Trim().Length > 150)
            {
                throw new ValidationException(
                    "Title must be between 3 and 150 characters.");
            }

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException(
                    "Description is required.");

            if (description.Trim().Length < 3 || description.Trim().Length > 500)
            {
                throw new ValidationException(
                    "Description must be between 3 and 500 characters.");
            }

            var dto = new TodoItemDto
            {
                Title = title.Trim(),
                Description = description.Trim(),
                DueDate = dueDate,
                CreatedAt = DateTime.Now,
                IsComplete = false,
                CompleteAt = null
            };

            await _todoRepository.Add(dto, cancellationToken);
        }


        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            TodoItemDto? todoItem = await _todoRepository.GetById(id,cancellationToken);

            if (todoItem is null)
            {
                throw new KeyNotFoundException(
                    $"Todo with id {id} was not found.");
            }

            if (todoItem.IsComplete)
            {
                throw new InvalidOperationException(
                    "Completed todo cannot be deleted.");
            }

            await _todoRepository.Delete(id,cancellationToken);
        }


        public async Task Complete(int id,CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.GetById(id,cancellationToken);

            if (todoItem is null)
            {
                throw new KeyNotFoundException(
                    $"Todo with id {id} was not found.");
            }

            if (todoItem.IsComplete)
            {
                throw new InvalidOperationException(
                    "Todo is already completed.");
            }
            await _todoRepository.Complete(id, cancellationToken);
        }



        public async Task Uncomplete(int id,CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.GetById(id,cancellationToken);

            if (todoItem is null)
            {
                throw new KeyNotFoundException(
                    $"Todo with id {id} was not found.");
            }

            if (!todoItem.IsComplete)
            {
                throw new InvalidOperationException(
                    "Todo is already uncompleted.");
            }
            await _todoRepository.Uncomplete(id,cancellationToken);
        }

        public async Task Update(int id,string title,string description,DateTime dueDate,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ValidationException(
                    "Title is required.");

            if (title.Trim().Length < 3 || title.Trim().Length > 150)
            {
                throw new ValidationException(
                    "Title must be between 3 and 150 characters.");
            }

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException(
                    "Description is required.");

            if (description.Trim().Length < 3 || description.Trim().Length > 500)
            {
                throw new ValidationException(
                    "Description must be between 3 and 500 characters.");
            }

            var dto = new TodoItemDto
            {
                Title = title.Trim(),
                Description = description.Trim(),
                DueDate = dueDate
            };

            await _todoRepository.Update(id,dto,cancellationToken);
        }

    }
}
