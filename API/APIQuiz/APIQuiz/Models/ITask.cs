﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIQuiz.Models
{
    public interface IFullTask
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
