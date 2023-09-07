using NLog;
using System.Reflection;
using TaskManagementSystem.Context;
using TaskManagementSystem.Exception;

namespace TaskManagementSystem.Business
{
    // Gestire l'eccezione del GUID
    public class TaskManagmentBusinessController
    {
        public static List<Models.Task> GetAllTask() {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                return db.Tasks.ToList();
            }
        }

        public static Models.Task GetTask(Guid id)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                Models.Task task = db.Tasks.Where(x => x.Id.ToString().Equals(id.ToString())).FirstOrDefault();
                if (task == null) throw new TaskManagmentException("Provide ID doesn't exists");
                return task;
            }
        }

        public static Models.Task AddTask(Models.Task task)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return task;
            }
        }

        public static Models.Task UpdateTask(Models.Task task)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                Models.Task dbTask = db.Tasks.Where(x => x.Id.ToString().Equals(task.Id.ToString())).FirstOrDefault();
                if (dbTask == null) throw new TaskManagmentException("Provide ID doesn't exists");
                if (dbTask.Type != task.Type) throw new TaskManagmentException("The task type can't be update.");
                db.Tasks.Update(task);
                db.SaveChanges();
                return task;
            }
        }
        public static bool DeleteTask(Guid id)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                Models.Task task = db.Tasks.Where(x => x.Id.ToString().Equals(id.ToString())).FirstOrDefault();
                if (task == null) throw new TaskManagmentException("Provide ID doesn't exists");
                db.Tasks.Remove(task);
                db.SaveChanges();
                return true;
            }
        }


        public static List<Models.Task> Search1(string description)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                return db.Tasks.Where(x=> x.Description.Contains(description) && 
                        (x.Type == Models.Task.TypeOfWork.Work || x.Type == Models.Task.TypeOfWork.Leisure)
                        && x.DueDate >=  DateTime.Now && x.DueDate < DateTime.Now.AddDays(7)
                    ).ToList();
            }
        }

        public static List<Models.TaskCount> Search2()
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                List<Models.TaskCount> query = (from task in db.Tasks.Where(x => x.DueDate < DateTime.Now && x.IsCompleted == false)
                        group task by task.Type into grp
                        select new Models.TaskCount { Type = grp.Key, Count = grp.Count() }).ToList() ;
                return query;

            }
        }

        public static List<Models.Task> Search3(string description, bool isWork, bool isLeasure, bool isHome)
        {
            using (TaskManagementContext db = new TaskManagementContext())
            {
                List<Models.Task.TypeOfWork> lists = new List<Models.Task.TypeOfWork>();
                if (isHome) lists.Add(Models.Task.TypeOfWork.Home);
                if (isLeasure) lists.Add(Models.Task.TypeOfWork.Leisure);
                if (isWork) lists.Add(Models.Task.TypeOfWork.Work);

                return db.Tasks.Where(x => (String.IsNullOrEmpty(description) == true ? x.Description != null : x.Description.Contains(description)) &&
                        lists.Contains(x.Type)).ToList();
            }
        }
    }
}
