namespace EnviroooProject.Models
{
    public class ManagerViewModel
    {
		public IEnumerable<MyErrand> Errands { get; set; }
		public IEnumerable<ErrandStatus> ErrandStatuses { get; set; } 
		public IEnumerable<Employee> Employees { get; set; } 
		

	}
}
