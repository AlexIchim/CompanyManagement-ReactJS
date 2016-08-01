using Domain.Models;
using System.Collections.Generic;

namespace Manager.InfoModels
{
    public class ProjectInfo
    {
        public string Name { get; set; }
        public int NrMembers { get; set; }
        public string Duration { get; set; }
        public virtual Status Status { get; set; }
    }
}
