using App.Domain.Core.Entity;

namespace App.Domain.Core.Entity
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? CompleteAt { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
