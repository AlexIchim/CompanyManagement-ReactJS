using Domain.Models;

namespace Manager.InputInfoModels
{
    public class AddDepartmentInputInfo
    {
        public string Name { get; set; }
        public  DepartmentManager { get; set; }
        public Office Office { get; set; }
    }
}
