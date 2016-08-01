using Domain.Models;
using System.Collections.Generic;

namespace Manager.InfoModels
{
    public class ProjectInfo
    {
        public ProjectInfo() { }

        public ProjectInfo(string name, int nrMembers, string duration, Status status)
        {
            this.Name = name;
            this.NrMembers = nrMembers;
            this.Duration = duration;
            this.Status = status;

        }
       

        public string Name { get; set; }
        public int NrMembers { get; set; }
        public string Duration { get; set; }
        public Status Status { get; set; }
    }
}
