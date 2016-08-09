using System.Collections.Generic;

namespace Domain.Models
{
    public class Office
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
