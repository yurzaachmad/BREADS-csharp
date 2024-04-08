namespace myFirstWeb.Models.Domain
{
    public class UpdateEmployeeProjectViewModel
    {
        public Guid ID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Boolean isComplete { get; set; }
        public Guid Developments { get; set; }
        public Guid Employee { get; set; }
    }
}
