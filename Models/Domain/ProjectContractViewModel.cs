namespace myFirstWeb.Models.Domain
{
    public class ProjectContractViewModel
    {
        public Guid ID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeName { get; set; }
        public string ClientName { get; set; }
        public Boolean isComplete { get; set; }
    }
}
