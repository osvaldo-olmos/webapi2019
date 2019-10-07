using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Dto;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ApplicationDbContext context, IMapper mapper,
                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems =await _context.TodoItems.Include(x => x.Responsible).ToListAsync();
            return todoItems;
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(NewTodoItemDTO itemDTO)
        {
            var item =_mapper.Map<TodoItem>(itemDTO);
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // PATCH: api/Todo/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(long id, UpdateTodoItemDTO changes)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            switch (changes.Field)
            {
                case "Name":
                    todoItem.Name =changes.Value;
                    break;

                case "IsCompleted":
                    todoItem.IsComplete = bool.Parse(changes.Value);
                    break;

                case "Responsible":
                    var user = await _userManager.Users.Where( u => u.Id == changes.Value).FirstOrDefaultAsync();
                    todoItem.Responsible =user;
                    break;
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("search")]
        public ActionResult<List<TodoItem>> Get(string searchString)
        {
            List<TodoItem> result = null;
            if(searchString ==null)
            {
                result =_context.TodoItems.ToList();
            }else
            {
                result =_context.TodoItems.Where(x => x.Name.ToLowerInvariant().Contains(searchString.ToLowerInvariant())).ToList();
            }
            return result;
        }
    }
}