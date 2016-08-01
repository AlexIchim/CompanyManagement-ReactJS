﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InfoModels
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Allocation { get; set; }
        
    }
}
