namespace TestRunner.Models
{
    public interface IQuizTask
    {
        int Id { get; set; }
        string Name { get; set; }
        string ShortDescription { get; set; }
        string FullDescription { get; set; }

    }
}
