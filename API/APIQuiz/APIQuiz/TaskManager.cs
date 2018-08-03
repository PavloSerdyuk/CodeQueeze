using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIQuiz.Models;

namespace APIQuiz
{
    public class TaskManager:ITasksManager
    {
        private readonly IFullTask _task;
        private readonly int _numberOfTasks;
        private int currentTaskNumber;
        private string[] tests;
        private string[] results;

        public TaskManager()
        {
            //*********************************
            var path = @"D:\Exoft\Code\API\APIQuiz\APIQuiz\bin\Debug\netcoreapp2.1\Number.txt";

            tests = null;
            results = null;
            currentTaskNumber = 0;

            if (!File.Exists(path))
            {
                _numberOfTasks = Convert.ToInt32(File.ReadAllLines(path).ToString());
            }
            else
            {
                _numberOfTasks = 0;
            }
        }

        public IFullTask GetTask()
        {
            throw new NotImplementedException();
        }

        public CheckTaskResponse CheckTask(CheckTaskRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
