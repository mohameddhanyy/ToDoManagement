using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODO.Data.Models;
using TODO.Data;
using TODO.DTOs;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TodosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // api/Todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoDto>>> GetTodos([FromQuery] ToDoFilterDto filter)
        {
            var query = _context.Todos.AsQueryable();

            // Apply filters
            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Status == filter.Status.Value);
            }

            if (filter.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == filter.Priority.Value);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= filter.EndDate.Value);
            }

            var todos = await query.ToListAsync();

            var todoDtos = todos.Select(t => new ToDoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                CreatedDate = t.CreatedDate,
                LastModifiedDate = t.LastModifiedDate
            });

            return Ok(todoDtos);
        }

        // api/Todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoDto>> GetTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            var todoDto = new ToDoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                CreatedDate = todo.CreatedDate,
                LastModifiedDate = todo.LastModifiedDate
            };

            return todoDto;
        }

        //  api/Todos
        [HttpPost]
        public async Task<ActionResult<ToDoDto>> CreateTodo(CreateToDoDto createTodoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = new ToDo
            {
                Id = Guid.NewGuid(),
                Title = createTodoDto.Title,
                Description = createTodoDto.Description,
                Status = createTodoDto.Status,
                Priority = createTodoDto.Priority,
                DueDate = createTodoDto.DueDate,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            var todoDto = new ToDoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                CreatedDate = todo.CreatedDate,
                LastModifiedDate = todo.LastModifiedDate
            };

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todoDto);
        }

        // api/Todos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, UpdateToDoDto updateTodoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = updateTodoDto.Title;
            todo.Description = updateTodoDto.Description;
            todo.Status = updateTodoDto.Status;
            todo.Priority = updateTodoDto.Priority;
            todo.DueDate = updateTodoDto.DueDate;
            todo.LastModifiedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // api/Todos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // api/Todos/5/complete
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Status = TodoStatus.Completed;
            todo.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}

