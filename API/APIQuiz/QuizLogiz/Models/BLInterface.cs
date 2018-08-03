using System.Threading.Tasks;
using TestRunner.Logic;

namespace TestRunner.Models
{
    public interface IBlInterface
    {
        IQuizTask GetTask(int id, ConfigurationPaths paths);
        CheckTaskResponse CheckCode(CheckTaskRequest request, ConfigurationPaths paths);
    }
}
