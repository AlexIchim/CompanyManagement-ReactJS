namespace Manager.InfoModels
{
    public class DepartmentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int NbrOfEmployees { get; set; }

        public int NbrOfProjects { get; set; }

        public string DepartmentManager { get; set; }
    }
}
