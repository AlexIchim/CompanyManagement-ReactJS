using System.Collections.Generic;

namespace Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Duration { get; set; }

        public Department Department { get; set; }

        public ICollection<ProjectAllocation> Allocations { get; set; }
    }
}
