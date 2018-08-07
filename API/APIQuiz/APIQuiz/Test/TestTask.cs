using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRunner.Models;

namespace APIQuiz.Test
{
    public class TestTask:IQuizTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }

        public TestTask()
        {
            Id = 1;
            Name = "Is this a triangle?";
            ShortDescription =
                "short description";
            FullDescription =
                "Implement a method that accepts 3 integer values a, b, c. The method should return true if a " +
                "triangle can be built with the sides of given length and false in any other case.";
        }

        
    }
}
