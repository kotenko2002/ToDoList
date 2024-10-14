using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private static List<Todo> todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Купити продукти", Description = "Купити молоко, хліб, яйця", IsCompleted = false },
            new Todo { Id = 2, Title = "Завдання на роботі", Description = "Завершити звіт до п'ятниці", IsCompleted = false }
        };

        public IActionResult Index()
        {
            return View(todos);
        }

        public IActionResult Details(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = todos.Count + 1;
                todo.IsCompleted = false;
                todos.Add(todo);
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        public IActionResult Edit(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            if (ModelState.IsValid)
            {
                var existingTodo = todos.FirstOrDefault(t => t.Id == todo.Id);
                if (existingTodo == null)
                {
                    return NotFound();
                }

                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;

                return RedirectToAction("Index");
            }

            return View(todo);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todos.Remove(todo);
            }
            return RedirectToAction("Index");
        }
    }
}
