using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InfoModels
{
    public class AssignmentInfo
    {
        public AssignmentInfo(string name, Position position, int allocation)
        {
            this.Name = name;
            this.Position = position;
            this.Allocation = allocation;
        }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Allocation { get; set; }
    }
}
