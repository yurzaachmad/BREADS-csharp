using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models.Domain
{
    public class Development
    {
        public Guid ID { get; set; }
        public string DevelopmentName { get; set; }
        public string ProjectManager { get; set; }
        [ForeignKey("Client")]
        public Guid? ClientID { get; set; }
    }
}
