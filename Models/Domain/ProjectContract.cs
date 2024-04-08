using System.ComponentModel.DataAnnotations.Schema;

namespace myFirstWeb.Models.Domain
{
    public class ProjectContract
    {
        public Guid ID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Boolean isComplete { get; set; }
  //      [ForeignKey("Developments")]
        public Guid Developments { get; set; }
     //   [ForeignKey("Employee")]
        public Guid Employee { get; set; }
    }
}
