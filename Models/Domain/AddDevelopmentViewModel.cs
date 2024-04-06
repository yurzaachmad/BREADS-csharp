using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models.Domain
{
	public class AddDevelopmentViewModel
	{
		public string ProjectName { get; set; }
		public string ProjectManager { get; set; }

        [ForeignKey("Client")]
        public Guid? SelectedID { get; set; }
	}
}
