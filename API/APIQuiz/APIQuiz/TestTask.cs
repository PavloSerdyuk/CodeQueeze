using APIQuiz.Models;

namespace APIQuiz
{
    public class TestTask: IFullTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceCode { get; set; }

        public TestTask()
        {
            Id = 1;
            Name = "Grasshopper - Terminal game combat function";
            Description = "Create a combat function that takes the player's current health and the " +
                          "amount of damage recieved, and returns the player's new health.Health can't be less than 0.";
            SourceCode = "using System.Collections.Generic;";
        }
    }
}
