using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InputInfoModels
{
    public class AddProjectInputInfo
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public int? Duration { get; set; }

        public int? DepartmentId { get; set; }

      
    }
}
