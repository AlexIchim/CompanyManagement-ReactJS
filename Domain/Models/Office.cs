using System.Collections.Generic;

namespace Domain.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
