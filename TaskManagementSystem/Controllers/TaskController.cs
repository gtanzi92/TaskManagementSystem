using Microsoft.AspNetCore.Mvc;
using NLog;
using TaskManagementSystem.Business;

namespace TaskManagementSystem.Controllers
{
    //This will be your main backend controller
    [ApiController]
    public class TaskController : ControllerBase
    {
        /* DBContext Injection. I don't really like this approch.
         * 
         * private readonly TaskManagementSystem.Context.TaskManagementContext db;

        public TaskController(TaskManagementSystem.Context.TaskManagementContext taskManagementContext)
        {
            this.db = taskManagementContext;
        }*/

        [AcceptVerbs("GET")]
        [Route("api/tasks")]
        public IActionResult GetAll()
        {
            return Ok(TaskManagmentBusinessController.GetAllTask());
        }

        [AcceptVerbs("GET")]
        [Route("api/tasks/{id}")]
        public IActionResult Get(string id)
        {
            return Ok(TaskManagmentBusinessController.GetTask(Guid.Parse(id)));
        }

        [AcceptVerbs("POST")]
        [Route("api/tasks")]
        public IActionResult Post([FromBody] Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Models.Task newTask = TaskManagmentBusinessController.AddTask(task);
            return Created(nameof(newTask), newTask);
        }

        [AcceptVerbs("PUT")]
        [Route("api/tasks/{id}")]
        public IActionResult Put(Guid id, [FromBody] Models.Task uTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Cannot create directly Guid from constructor
            uTask.SetGuid(id);
            Models.Task task = TaskManagmentBusinessController.UpdateTask(uTask);
            return Ok(task);
        }

        [AcceptVerbs("DELETE")]
        [Route("api/tasks/{id}")]
        public IActionResult Delete(string id)
        {
            TaskManagmentBusinessController.DeleteTask(Guid.Parse(id));
            return Ok();
        }


        [AcceptVerbs("GET")]
        [Route("api/tasks/search1")]
        public IActionResult Search1(string description)
        {
            return Ok(TaskManagmentBusinessController.Search1(description));
        }

        [AcceptVerbs("GET")]
        [Route("api/tasks/search2")]
        public IActionResult Search2()
        {
            return Ok(TaskManagmentBusinessController.Search2());
        }


        [AcceptVerbs("GET")]
        [Route("api/tasks/search3")]
        public IActionResult Search3(string description, bool isWork, bool isLeasure, bool isHome)
        {
            return Ok(TaskManagmentBusinessController.Search3(description, isWork, isLeasure, isHome));
        }
    }
}
