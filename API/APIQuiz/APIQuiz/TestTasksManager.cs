using System;
using APIQuiz.Models;

namespace APIQuiz
{
    public class TestTasksManager : IBlInterface
    {
        private readonly IFullTask _task;

        public CheckTaskResponse CheckTask(CheckTaskRequest request)
        {
            return new CheckTaskResponse{Id = 1,Result = true};
        }

        public CheckTaskResponse CheckCode(CheckTaskRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
