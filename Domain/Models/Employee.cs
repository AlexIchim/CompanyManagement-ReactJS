using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public enum Position
    {
        Developer,
        ProjectManager,
        QA,
        ProductOwner
    };
    public enum JobTypes 
    {
        partTime4,
        partTime6,
        fullTime
    };
    public class Employee {
        public Employee()
        {
            this.Assignments = new List<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public JobTypes JobType { get; set; }
        public Position Position { get; set; }

        public int GetRemainingAllocation() {
            return 100 - GetAllocation();
        }

        public int GetAllocation() {
            int totalAllocation = 0;
            foreach (var assignment in Assignments) {
                totalAllocation += assignment.Allocation;
            }
            return totalAllocation;
        }
    }
}
