using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class FullTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CurrentTask : FullTask
    {
        public string Code;
        public bool Completed;

        public CurrentTask()
        {
            Id = 1;
            Name = "";
            Description = "";
            Code = "";
            Completed = false;
        }

    }
}
