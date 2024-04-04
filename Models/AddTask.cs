namespace myFirstWeb.Models
{
    public class AddTask
    {
        public Guid ID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public bool Complete { get; set; }
        public Guid SelectedEmployeeId { get; set; }
    }
}
