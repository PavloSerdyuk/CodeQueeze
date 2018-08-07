using TestRunner.Models;

namespace TestRunner.Logic
{
    public class QuizTask : IQuizTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
    }
}
