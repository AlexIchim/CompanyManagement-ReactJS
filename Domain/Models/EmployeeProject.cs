namespace Domain.Models
{
    public class EmployeeProject
    {
        public EmployeeProject()
        {
            
        }

        public EmployeeProject(Employee e, Project p,int allocation)
        {
            this.EmployeeId = e.Id;
            this.Employee = e;
            this.ProjectId = p.Id;
            this.Project = p;
            this.Allocation = allocation;
        }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int Allocation { get; set; }
    }
}
