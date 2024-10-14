using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _context.Todos.ToListAsync();
            return View(todos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
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
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.IsCompleted = false;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo todo)
        {
            if (ModelState.IsValid)
            {
                var existingTodo = await _context.Todos.FindAsync(todo.Id);
                if (existingTodo == null)
                {
                    return NotFound();
                }

                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
