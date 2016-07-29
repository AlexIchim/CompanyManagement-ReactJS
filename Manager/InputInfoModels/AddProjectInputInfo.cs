using System;
using Domain.Models;

namespace Manager.InputInfoModels
{
    public class AddProjectInputInfo
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public string Duration { get; set; }
        public Status Status { get; set; }
    }
}
