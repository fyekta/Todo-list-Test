using App.Domain.Core.Dtos;
using App.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.Service
{
   
    public interface ITodoService
    {
        Task<List<TodoItemDto>> GetAll(CancellationToken cancellationToken);

        Task<TodoItemDto> GetById(int id,CancellationToken cancellationToken);

        Task Add(string title, string description, DateTime dueDate, CancellationToken cancellationToken);

        Task Delete(int id, CancellationToken cancellationToken);
    }
}
