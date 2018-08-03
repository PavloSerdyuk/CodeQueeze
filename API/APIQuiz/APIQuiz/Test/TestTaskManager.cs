using System;
using System.Collections.Generic;
using System.Linq;
using TestRunner.Models;

namespace APIQuiz.Test
{
    public class TestTaskManager:IBlInterface
    {
        public IQuizTask GetTask(int id)
        {
            return new TestTask() {Id = id};
        }

        public CheckTaskResponse CheckCode(CheckTaskRequest request)
        {
            return new CheckTaskResponse() {Id = 1, Message = "Good!", Result = true};
        }
    }
}
