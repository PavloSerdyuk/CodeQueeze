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

        public TaskManager()
        {
            
        }

        public ITask GetTask()
        {
            throw new NotImplementedException();
        }

        public CheckTaskResponse CheckTask(CheckTaskRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
