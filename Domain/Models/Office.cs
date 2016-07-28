using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models {
    public class Office {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Department> Departments { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
    }
}
