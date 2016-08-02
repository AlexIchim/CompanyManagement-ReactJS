using Domain.Models;
using System;
using System.Collections.Generic;
namespace Manager.InfoModels
{
    public class ProjectMemberInfo
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Allocation { get; set; }
    }
}
