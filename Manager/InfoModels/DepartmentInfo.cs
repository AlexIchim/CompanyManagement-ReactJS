using System.Collections.Generic;
using Domain.Models;

namespace Manager.InfoModels
{
    public class DepartmentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OfficeId { get; set; }

        //custom: 
        public string DepartmentManagerName { get; set; }
        public int EmployeeCount { get; set; }
        public int ProjectCount { get; set; }
    }
}
