using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public enum Status {
        NotStartedYet,
        InProgress,
        OnHold,
        Finished,
    };
    public class Project
    {
        public Project()
        {
            this.Assignments = new List<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public string Duration { get; set; }
        public Status Status { get; set; }
    }
}
