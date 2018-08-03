using System;
using APIQuiz.Models;

namespace APIQuiz
{
    public class TestTasksManager : ITasksManager
    {
        private readonly IFullTask _task;

        
        public TestTasksManager()
        {
            _task = new TestTask();
        }

        public IFullTask GetTask()
        {
            return _task;
        }

        public CheckTaskResponse CheckTask(CheckTaskRequest request)
        {
            return new CheckTaskResponse{Id = 1,Result = true};
        }
    }
}
