using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIQuiz.Models
{
    public interface ITasksManager
    {
        ITask GetTask();
        CheckTaskResponse CheckTask(CheckTaskRequest request);
    }
}
