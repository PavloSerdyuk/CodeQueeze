using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class AdminViewModel
    {
        public string Code { get; set; }
        public string  Tests{ get; set; }
        public string Name{ get; set; }
        public string Results { get; set; }
        public string Description{ get; set; }
        public string ShortDescription { get; set; }
    }
}
