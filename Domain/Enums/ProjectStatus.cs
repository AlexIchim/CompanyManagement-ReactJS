using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ProjectStatus
    {
        [Description("Done")]
        Done = 1,

        [Description("On hold")]
        OnHold = 2,

        [Description ("In progress")]
        InProgress = 3
    }
}
