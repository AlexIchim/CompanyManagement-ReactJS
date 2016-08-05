using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum PositionType
    {
        [Description("Developer")]
        Developer=1,

        [Description("Project Manager")]
        ProjectManager=2,

        [Description("Team Lead")]
        TeamLead=3,

        [Description("QA")]
        QA=4,

        [Description("BA")]
        BA=5,

        [Description("Department Manager")]
        DepartmentManager=6
    }
}
