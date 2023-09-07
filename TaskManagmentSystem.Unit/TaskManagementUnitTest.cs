using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Exception;

namespace TaskManagmentSystem.Unit
{
    [TestClass]
    public class TaskManagementUnitTest
    {

        //Creation Task
        [TestMethod]
        public void CreationTask()
        {
            TaskManagementSystem.Controllers.TaskController controller = new TaskManagementSystem.Controllers.TaskController();
            TaskManagementSystem.Models.Task t = new TaskManagementSystem.Models.Task();
            t.Title = "Test 1";
            t.Description = "Descrizione random";
            t.DueDate = DateTime.Now.AddDays(5);
            t.IsCompleted = true;
            t.Type = TaskManagementSystem.Models.Task.TypeOfWork.Leisure;
            Microsoft.AspNetCore.Mvc.CreatedResult http = (Microsoft.AspNetCore.Mvc.CreatedResult) controller.Post(t);
            if (http.StatusCode != 201) throw new Exception("TaskController - Insert case failed");
        }
     
        [TestMethod]
        public void UpdateTaskType()
        {
            TaskManagementSystem.Controllers.TaskController controller = new TaskManagementSystem.Controllers.TaskController();
            Microsoft.AspNetCore.Mvc.OkObjectResult okResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)controller.GetAll();
            TaskManagementSystem.Models.Task t = ((List<TaskManagementSystem.Models.Task>)okResult.Value).First();
            if (t.Type != TaskManagementSystem.Models.Task.TypeOfWork.Home)
            {
                t.Type = TaskManagementSystem.Models.Task.TypeOfWork.Home;
            }
            else
            {
                t.Type = TaskManagementSystem.Models.Task.TypeOfWork.Leisure;
            }
            try
            {
                Microsoft.AspNetCore.Mvc.CreatedResult http = (Microsoft.AspNetCore.Mvc.CreatedResult)controller.Put(t.Id, t);
            }
            catch (Exception e) { 
                if(!(e is TaskManagmentException ex))
                {
                    throw new Exception("TaskController - Task type can't be update");
                }
            }
        }

        [TestMethod]
        public void UpdateGhostTask()
        {
            TaskManagementSystem.Controllers.TaskController controller = new TaskManagementSystem.Controllers.TaskController();
            try
            {
                Microsoft.AspNetCore.Mvc.OkObjectResult okResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)controller.GetAll();
                TaskManagementSystem.Models.Task t = ((List<TaskManagementSystem.Models.Task>)okResult.Value).First();
                Microsoft.AspNetCore.Mvc.CreatedResult http = (Microsoft.AspNetCore.Mvc.CreatedResult)controller.Put(Guid.NewGuid(), t);
            }
            catch (Exception e)
            {
                if (!(e is TaskManagmentException ex))
                {
                    throw new Exception("TaskController - The provied task doesn't exists but the system answered with positive status code or not correct exception");
                }
            }
        }



        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}