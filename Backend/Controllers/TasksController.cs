using Backend.Dtos;
using Backend.Helper;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;

        }

        [HttpGet]
        // GET: api/Tasks
        // Using ActionResult<T> As Return Type To Provide More Flexibility In Responses.
        // And To Define The Type Of Data Being Returned (IEnumerable<Task> In This Case).
        public ActionResult<IEnumerable<TaskItem>> GetAllTasks()
        {
            // Directly Return The Result Of GetAll Method From Repository.
            return Ok(taskRepository.GetAll());

            // Instead Of This.
            //var tasks = taskRepository.GetAll();
            //return Ok(tasks) ;
        }
        [HttpGet("{id:int}")]
        // GET: api/Tasks/1
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = taskRepository.GetById(id);
            // Check If Task Is Null And Return NotFound If It Is.
            if(task == null)
                return NotFound(new GeneralErrorResponse(404,"Task Not Found"));
            // Return The Found Task.
            return Ok(task);
        }

        [HttpPost]
        //POST: api/Tasks
        public ActionResult CreateTask([FromBody] AddTaskDto taskDto)
        {
            // CReate New Task With The Same Info Of Given Task
            var task = new TaskItem
            {
                Title = taskDto.Title ?? "",
                Description = taskDto.Description,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false // New Task Is Not Completed By Default.

            };
            taskRepository.Add(task);

            // Returns a 201 Created Response And Include The Created Task In The Body 
            // And The URL To Access It In 
            return CreatedAtAction(nameof(GetTaskById),new { id = task.Id },task);
        }

        [HttpPut("{id:int}")]
        // PUT: api/Tasks/1
        public ActionResult UpdateTask(int id,[FromBody] UpdateTaskDto taskDto)
        {
            // Check If This Task Is Already Exists
            var taskToUpdate = taskRepository.GetById(id);
            if(taskToUpdate == null)
                return NotFound(new GeneralErrorResponse(404,"Task Not Found!"));

            // Mapping From Dto To Domain Model
            taskToUpdate.Title = taskDto.Title ?? taskToUpdate.Title; // To Keep Old Data If No New Data Sent
            taskToUpdate.Description = taskDto.Description ?? taskToUpdate.Description;

            // Only Update IsCompleted If Is Has Value
            if(taskDto.IsCompleted.HasValue)
                taskToUpdate.IsCompleted = taskDto.IsCompleted.Value;

            // Update The Repository
            taskRepository.Update(taskToUpdate);

            return NoContent();

        }

        // DELETE: api/Tasks/1
        [HttpDelete("{id:int}")]
        public ActionResult DeleteTask(int id)
        {
            var taskToDelete = taskRepository.GetById(id);
            if(taskToDelete == null)
                return NotFound(new GeneralErrorResponse(404,"Task Not Found!"));
            taskRepository.Delete(id);
            return NoContent();
        }
    }
}
