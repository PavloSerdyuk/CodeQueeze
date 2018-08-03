using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIQuiz.Models
{
    public interface IBlInterface
    {
        Task GetTask(int id);
        CheckTaskResponse CheckCode(CheckTaskRequest request);
    }
}
