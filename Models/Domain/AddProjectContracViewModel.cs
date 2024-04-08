using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models.Domain
{
    public class AddProjectContracViewModel
    {
        public Guid ID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Boolean isComplete { get; set; }
        public Guid IdProject { get; set; }
        public Guid IdEmployee { get; set; }
    }
}
