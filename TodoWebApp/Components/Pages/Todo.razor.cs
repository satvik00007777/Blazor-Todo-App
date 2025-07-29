using TodoWebApp.Models;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Components.QuickGrid

namespace TodoWebApp.Components.Pages
{
    public partial class Todo
    {
        private List<TodoModel> todos = [];
        private string newTodoItem = "";
        //private PaginationState pagination = new() { ItemsPerPage = 10 };

        private void AddTodoItem()
        {
            if (!string.IsNullOrWhiteSpace(newTodoItem))
            {
                todos.Add(new TodoModel { Title = newTodoItem });
                newTodoItem = string.Empty;
            }
        }
    }
}
