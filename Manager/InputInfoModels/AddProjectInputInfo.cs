using Domain.Models;

namespace Manager.InputInfoModels
{
    public class AddProjectInputInfo
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Duration { get; set; }

        public int DepartmentId { get; set; }
    }
}
