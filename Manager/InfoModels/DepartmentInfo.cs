using System.Collections.Generic;
using Domain.Models;

namespace Manager.InfoModels
{
    public class DepartmentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OfficeId { get; set; }
    }
}
