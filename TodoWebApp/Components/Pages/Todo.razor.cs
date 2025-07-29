using TodoWebApp.Models;
using System.Collections.Generic;

namespace TodoWebApp.Components.Pages
{
    public partial class Todo
    {
        private List<TodoModel> todos = [];
        private string newTodoItem = "";

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
