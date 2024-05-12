using DemoTodo.DataBase;
using DemoTodo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoTodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;
        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }


        //Post API Request
        [HttpPost]
        public async Task<IActionResult> AddTodo(TodoTasks todo)
        {
            todo.Id = Guid.NewGuid();
            todo.Status = false;
            _todoDbContext.Todo.Add(todo);
            await _todoDbContext.SaveChangesAsync();//we need save the change that made to mysql table

            return Ok(todo);
        }

        //Get API Request
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos=await _todoDbContext.Todo.ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTodoTask([FromRoute] Guid id)
        {
           var todo=await  _todoDbContext.Todo.FirstOrDefaultAsync(x => x.Id == id);

            if(todo == null)
            {
                return NotFound();
            }
            return Ok(todo);

        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> updateTodoTask([FromRoute] Guid id, TodoTasks upddatedTodoTaskRequest)
        {
            var todo = await _todoDbContext.Todo.FindAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
                todo.Name = upddatedTodoTaskRequest.Name;
                todo.Description = upddatedTodoTaskRequest.Description;
                todo.Status = upddatedTodoTaskRequest.Status;
                
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> deleteTodo([FromRoute] Guid id)
        {
            var findTask = await _todoDbContext.Todo.FindAsync(id);
            if(findTask == null)
            {
                return NotFound();
            }

            _todoDbContext.Todo.Remove(findTask);
            await _todoDbContext.SaveChangesAsync();
            return Ok(findTask);
        }

    }
}
