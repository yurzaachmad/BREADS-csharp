namespace myFirstWeb.Models
{
	public class UpdateTaskViewModel
	{
		public Guid ID { get; set; }
		public string TaskName { get; set; }
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
		public bool Complete { get; set; }
		public Guid Employee { get; set; }
	}
}
