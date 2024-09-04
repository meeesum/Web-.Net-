namespace TechHub.Models.Entities
{
    public class UserDashboardViewModel
    {
        public string UserName { get; set; }
        public List<Project> Projects { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
