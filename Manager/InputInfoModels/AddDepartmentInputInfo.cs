using Domain.Models;

namespace Manager.InputInfoModels
{
    public class AddDepartmentInputInfo
    {
        public int OfficeId { get; set; }
        public string Name { get; set; }

        public int? DepartmentManagerId { get; set; }
    }
}
