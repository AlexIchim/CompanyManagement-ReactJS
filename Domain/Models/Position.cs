using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
