using App.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public ICollection<TodoItem> TodoItems { get; set; }= new List<TodoItem>();

    }
}
