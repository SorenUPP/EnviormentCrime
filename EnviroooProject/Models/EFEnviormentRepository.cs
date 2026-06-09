
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnviroooProject.Models

{
	//This is the main repository for the program which also takes responsibility for all saving, documenting and uploding methods which will be used around all controllers in general
	public class EFEnviormentRepository : IEnviormentRepository
	{
		private readonly ApplicationDbContext context;
		private readonly IHttpContextAccessor contextAccessor;

		public EFEnviormentRepository(ApplicationDbContext ctx, IHttpContextAccessor contextAcc)
		{
			context = ctx;
			contextAccessor = contextAcc;
		}

		public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e =>e.Pictures);
		public IQueryable<Department> Departments => context.Departments;
		public IQueryable<Employee> Employees => context.Employees;
		public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
		public IQueryable<Picture> Pictures => context.Pictures;
		public IQueryable<Sample> Samples => context.Samples;
		public IQueryable<Sequence> Sequences => context.Sequences;

		// This method taes responibility to save the errand in the repository / database (main usage in the form saving of citizen / cordinator controller
		public void SaveErrand(Errand errand)
		{
			if (errand.ErrandId == 0)
			{
				var sequence = Sequences.FirstOrDefault(s => s.Id == 1);
				
				errand.RefNumber = DateTime.Now.Year + "-45-" + sequence.CurrentValue;
				errand.StatusId = "S_A";
				
				context.Errands.Add(errand);

				sequence.CurrentValue++;
			
				context.SaveChanges();
			}
			
		}
			
		// This method is a key method which fethces the corresponding errand id for the used method or element
		public Errand GetErrandDetail(int id)
		{
			return Errands.FirstOrDefault(e => e.ErrandId == id);
		}
	
		//this method takes the responsibility to update the department which is used in cordinator controller, it helps the cordinator admin to switch the corresponding department in the databse
		public void UpdateErrandToDepartment(int ErrandId, string DepartmentId)
		{
			var errand = GetErrandDetail(ErrandId);
			
			

			if (errand != null)
			{
				errand.DepartmentId = DepartmentId;
				context.SaveChanges();

			}

		}
		
		//this method takes responsibility for updating the manager which is mainly used in the manager section of the controllers
		//method takes the initiativs to change the status of the case / manager
		//it also has features to indicate if the case does need a manager or not with "noAction" checkbox with string "reason" being the motive for the choice
		public void UpdateErrandToManager(int ErrandId, string ChosenManager, bool noAction, string reason)
		{
			var managerUpdate = GetErrandDetail(ErrandId);

			if (noAction == false)
			{
				managerUpdate.EmployeeId = ChosenManager;
				managerUpdate.StatusId = "S_A";
				context.SaveChanges();
			}
			if (noAction == true)
			{
				managerUpdate.StatusId = "S_B";
				managerUpdate.InvestigatorInfo = reason;
				managerUpdate.EmployeeId = null;
				context.SaveChanges();
			}
		}
		//this method upates the investigator information, events and the given status by investigator
		public void UpdateErrandToInvestigator(int ErrandID, string ChosenStatus, string information, string events)
		{
			var investigatorUpdate = GetErrandDetail(ErrandID);

			if (investigatorUpdate != null)
			{
				investigatorUpdate.StatusId = ChosenStatus;
				investigatorUpdate.InvestigatorInfo += (!string.IsNullOrWhiteSpace(investigatorUpdate.InvestigatorInfo) ? " " : "") + information;
				investigatorUpdate.InvestigatorAction += (!string.IsNullOrWhiteSpace(investigatorUpdate.InvestigatorAction) ? " " : " ") + events;

				context.SaveChanges();		
			}
			
			
			
		}
		//this is a parallel method which is used in investigator controller for the sake of uploading files (pictures / samples) in the database
		public void UpdateFolderFiles(int ErrandID, string Filename, string tableName)
        {
			var FolderUptader = GetErrandDetail(ErrandID);
			if (Filename != null && tableName == "Pictures")
			{
				var newPicture = new Picture
				{
					PictureName = Filename,
					ErrandId = ErrandID,
					
				};
				
				FolderUptader.Pictures.Add(newPicture);
				context.SaveChanges();

			}
			if (Filename != null && tableName =="Samples")
			{
				var newSample = new Sample
				{
					SampleName = Filename,
					ErrandId= ErrandID,
				};
				FolderUptader.Samples.Add(newSample);
				context.SaveChanges();
			}

        }
		//this method fetches the right users for the right roles in the site section which is related to managers and investigators for them getting linked to their corresnponding department
        public IQueryable<MyErrand>  FetchErrandList()
        {
            var errandList = from err in Errands
                             join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                                 into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()
                             join em in Employees on err.EmployeeId equals em.EmployeeId
                                into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             orderby err.RefNumber descending

                             select new MyErrand
                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
								 DepartmentName = (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName),
                                 EmployeeName = (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };


			return errandList;
        }
		//this method fethces the manager department ID
		public string GetManagerDepartmentId()
		{
			var userName = contextAccessor.HttpContext.User.Identity.Name;
			var manager = Employees.FirstOrDefault(e => e.EmployeeId == userName);
			return manager.DepartmentId;
		}
		
		public IQueryable<Employee> FetchEmployeeByManagerDepartment()
		{
			var departmentId = GetManagerDepartmentId();
			var employeelist = Employees.Where(e => e.DepartmentId == departmentId);
			return employeelist;
		}
        public IQueryable<MyErrand> FetchErrandsManager()
        {
            var departmentId = GetManagerDepartmentId();

            string departmentName = Departments.Where(d => d.DepartmentId == departmentId).Select(d => d.DepartmentName)
                .FirstOrDefault(); 

            var errandList = FetchErrandList().Where(err => err.DepartmentName == departmentName);

            return errandList;
        }

		//this section fethces the department id

        public string GetInvestigatorDepartmentId()
		{
			var userName = contextAccessor.HttpContext.User.Identity.Name;
			var investigator = Employees.FirstOrDefault(e => e.EmployeeId == userName);
			return investigator.EmployeeId;
		}
		//this method uses fetcherrandlist method to get the right investigator for the right id
		public IQueryable<MyErrand> FetchErrandsInvestigator()
		{
			var investigatorId = GetInvestigatorDepartmentId();
			string investigatorName = Employees.Where(e => e.EmployeeId == investigatorId).Select(e => e.EmployeeName).FirstOrDefault();
			var errandList = FetchErrandList().Where(emm => emm.EmployeeName == investigatorName);
			return errandList;
		}



	}
}
