using Microsoft.AspNetCore.Mvc;

namespace EnviroooProject.Models
{
	public interface IEnviormentRepository
	{
			//Read
		IQueryable<Errand> Errands { get; }
		IQueryable<Department> Departments { get; }
		IQueryable<Employee> Employees { get; }
		IQueryable<ErrandStatus> ErrandStatuses { get; }

		IQueryable <Picture> Pictures { get; }
		IQueryable<Sequence> Sequences { get; }
		IQueryable<Sample> Samples { get; }

			//Create and update

		void SaveErrand(Errand errand);

		Errand GetErrandDetail(int id);

		void UpdateErrandToDepartment(int ErrandId, string DepartmentId);
		void UpdateErrandToManager(int ErrandId, string ChosenManager, bool noAction, string reason);

		void UpdateErrandToInvestigator(int ErrandID, string ChosenStatus,string information, string events);
		void UpdateFolderFiles(int ErrandID, string Filename, string tableName);

		IQueryable<MyErrand> FetchErrandList();

		string GetManagerDepartmentId();
		IQueryable<MyErrand> FetchErrandsManager();
		IQueryable<MyErrand> FetchErrandsInvestigator();
		IQueryable<Employee> FetchEmployeeByManagerDepartment();
		
	}

}
