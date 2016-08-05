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
            JobType e = (JobType)Enum.Parse(typeof(JobType), s);
            return e;
        }
        public static PositionType GetPositionTypeEnum(string s)
        {
            PositionType e = (PositionType)Enum.Parse(typeof(PositionType), s);
            return e;
        }
    }
}
