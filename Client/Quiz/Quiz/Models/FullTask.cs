using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class FullTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
    }

    public class CurrentTask : FullTask
    {
        [DataType(DataType.MultilineText)]
        public string Code { get; set; }
        public bool Completed { get; set; }

        public CurrentTask()
        {
            Id = 0;
            Name = "";
            ShortDescription = "";
            FullDescription = "";
            Code = "";
            Completed = false;
        }

    }
}
