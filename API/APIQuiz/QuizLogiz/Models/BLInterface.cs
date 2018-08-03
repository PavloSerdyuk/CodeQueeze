using System.Threading.Tasks;

namespace TestRunner.Models
{
    public interface IBlInterface
    {
        Task GetTask(int id);
        CheckTaskResponse CheckCode(CheckTaskRequest request);
    }
}
