﻿using Todo.Domain.common;

namespace Todo.Domain.Entities
{
    public class TodoList : BaseAuditableEntity
    {
        public string Title { get; set; }

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
