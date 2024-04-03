namespace myFirstWeb.Models
{
	public class Project
	{
		public Guid ID { get; set; }
		public string ProjectName { get; set; }
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
		public bool Status { get; set; }

		public Employee ID_employee { get; set; }

		//public Guid ID { get; set; }

	}
}
