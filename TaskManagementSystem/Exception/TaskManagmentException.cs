using System.Globalization;

namespace TaskManagementSystem.Exception
{
    public class TaskManagmentException : System.Exception
    {
        public TaskManagmentException() : base() { }

        public TaskManagmentException(string message) : base(message) { }

        public TaskManagmentException(string message, params object[] args): base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
