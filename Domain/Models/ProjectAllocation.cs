using System.Collections.Generic;

namespace Domain.Models
{
    public class ProjectAllocation
    {
        public int Id { get; set; }
        public int AllocationPercentage { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
