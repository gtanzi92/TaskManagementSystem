using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models
{
    //Incomplete implementation of the Task Object
    public class Task
    {
        /*
        The task should have the following properties: Id (Guid), Title (string), Description
        (string), DueDate (DateTime), IsCompleted (bool).
         */

        public enum TypeOfWork
        {
            Home, 
            Leisure, 
            Work
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title can't be more than 50 characters", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description can't be more than 1000 characters", MinimumLength = 1)]
        public string Description { get; set; }

        [Required]
        public TypeOfWork Type { get; set; }

        [Required]
        [Range(typeof(DateTime), "01/01/2023", "31/12/2099")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;


        public void SetGuid(Guid id) {
            this.Id = id;
        }
    }

    public class TaskCount {
        public Task.TypeOfWork Type { get; set; }
        public int Count { get; set; }
    }
}
