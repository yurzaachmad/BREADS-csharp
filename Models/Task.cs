using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models
{
	public class TaskClass
	{
		public Guid ID { get; set; }
		public string Task { get; set; }
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
		public bool Complete { get; set; }

		[ForeignKey("Employee")]
        public Guid? ID_employeeID { get; set; }

		//public Guid ID { get; set; }

	}
}
