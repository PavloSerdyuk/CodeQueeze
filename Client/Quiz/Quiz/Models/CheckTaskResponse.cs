using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class CheckTaskResponse
    {
        public int Id { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }

    public class CheckTaskRequest
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }

}
