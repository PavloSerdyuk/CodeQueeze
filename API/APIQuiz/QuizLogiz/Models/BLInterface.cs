using System.Threading.Tasks;
using TestRunner.Logic;

namespace TestRunner.Models
{
    public interface IBlInterface
    {
        IQuizTask GetTask(int id);
        CheckTaskResponse CheckCode(CheckTaskRequest request);
    }
}
