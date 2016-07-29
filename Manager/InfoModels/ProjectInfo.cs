using System.Collections.Generic;
using Domain.Models;

namespace Manager.InfoModels
{
    public class ProjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Duration { get; set; }

        public int? DepartmentId { get; set; }
    }
}
