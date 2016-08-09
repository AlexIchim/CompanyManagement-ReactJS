using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Manager.Descriptors
{
    public static class EnumDescriptor
    {
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static JobType GetJobTypeEnum(string s)
        {
            IEnumerable<string> names = Enum.GetNames(typeof(JobType));
            foreach (string name in names)
            {
                JobType jt = (JobType) Enum.Parse(typeof(JobType), name);
                if (jt.GetDescription() == s)
                    return jt;
            }
            return 0;
        }
        public static PositionType GetPositionTypeEnum(string s)
        {
            IEnumerable<string> names = Enum.GetNames(typeof(PositionType));
            foreach (string name in names)
            {
                PositionType pt = (PositionType)Enum.Parse(typeof(JobType), name);
                if (pt.GetDescription() == s)
                    return pt;
            }
            return 0;
        }
    }
}
