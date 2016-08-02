using Domain.Models;

namespace Manager.InfoModels
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Allocation { get; set; }
        
    }
}
