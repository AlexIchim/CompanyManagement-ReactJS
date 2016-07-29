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
        public Department Department { get; set; }
        public List<Assignment> Assignments { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public JobTypes JobType { get; set; }
        public Position Position { get; set; }
    }
}
