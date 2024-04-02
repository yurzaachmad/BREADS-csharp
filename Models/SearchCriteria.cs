namespace myFirstWeb.Models
{
    public class SearchCriteria
    {
        public string Name { get; set; }
		public string Email { get; set; }

		public long Salary { get; set; }

		public DateTime DateOfBirth { get; set; }
		public string Department { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
    }
}
