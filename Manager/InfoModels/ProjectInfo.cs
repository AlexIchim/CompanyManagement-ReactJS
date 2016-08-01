using Domain.Models;
using System.Collections.Generic;

namespace Manager.InfoModels
{
    public class ProjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Department { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }
}
