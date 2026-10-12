using App.Domain.Core.Contract.Appservice;
using App.Domain.Core.Contract.Service;
using App.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public class TodoAppService : ITodoAppService
    {
        private readonly ITodoService _todoService;

        public TodoAppService(ITodoService todoService)
        {
            _todoService = todoService;
        }


        public async Task<List<TodoItemDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _todoService.GetAll(cancellationToken);
        }


        public async Task<TodoItemDto?> GetById(int id,CancellationToken cancellationToken)
        {
            return await _todoService.GetById(id,cancellationToken);
        }


        public async Task Add(string title, string description, DateTime dueDate, CancellationToken cancellationToken)
        {
            await _todoService.Add(title,description,dueDate,cancellationToken);
        }


        public async Task Delete(int id,CancellationToken cancellationToken)
        {
            await _todoService.Delete(id,cancellationToken);
        }


        public async Task Complete(int id,CancellationToken cancellationToken)
        {
             await _todoService.Complete(id,cancellationToken);
        }

        public async Task Uncomplete(int id,CancellationToken cancellationToken)
        {
             await _todoService.Uncomplete(id,cancellationToken);
        }
        public async Task Update(int id,string title,string description,DateTime dueDate,CancellationToken cancellationToken)
        {
            await _todoService.Update(id,title,description,dueDate,cancellationToken);
        }
    }
}
