using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Manager.InputInfoModels
{
    public class AddProjectInputInfo
    {
        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public int? Duration { get; set; }

        public int? DepartmentId { get; set; }

      
    }
}
