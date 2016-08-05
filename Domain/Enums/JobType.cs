using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum JobType
    {
        [Description("Part time (4 hours/day)")]
        PartTime4=1,

        [Description("Part time (6 hours/day)")]
        PartTime6=2,

        [Description("Full time")]
        FullTime=3
    }
}
