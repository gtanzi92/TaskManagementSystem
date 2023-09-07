using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagmentSystem.Models;
using System;

namespace TaskManagmentSystem.UnitTest
{
    [TestClass]
    public class TaskUnitTest
    {
        [TestMethod]
        //The Id should be generated automatically upon task creation.
        public void CreationTaskGuid()
        {
            TaskController task = new TaskController();
        }
    }
}
