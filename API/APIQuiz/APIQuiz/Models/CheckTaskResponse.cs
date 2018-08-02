using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIQuiz.Models
{
    public class CheckTaskResponse
    {
        public int Id { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
