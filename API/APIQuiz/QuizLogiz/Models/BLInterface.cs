using System.Threading.Tasks;

namespace TestRunner.Models
{
    public interface IBlInterface
    {
        IQuizTask GetTask(int id, ConfigurationPaths paths);
        bool CreateTask(string name, string description, string shortDescription, string tests, string results, ConfigurationPaths paths);
        CheckTaskResponse CheckCode(CheckTaskRequest request, ConfigurationPaths paths);
    }
}
