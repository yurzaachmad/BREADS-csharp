using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models.Domain
{
    public class UpdateDevelopmentViewModel
    {
        public Guid ID { get; set; }
        public string DevelopmentName { get; set; }
        public string ProjectManager { get; set; }
        public Guid ClientID { get; set; }
    }
}
