using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIQuiz.Models;

namespace APIQuiz
{
    public class Task:ITask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
