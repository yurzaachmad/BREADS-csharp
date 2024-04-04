namespace myFirstWeb.Models
{
	public class TaskEmployee
	{
		public Guid ID { get; set; }
		public string ProjectName { get; set; }
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string EmployeeName { get; set; }
		public Boolean Complete { get; set; }
		public Guid ID_employeeID { get; set; }

	}
}
