using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EnviroooProject.Controllers
{

	//this controller handles manager section of the website which takes responsibility for the choices upon the intended investigators for the cases
	//this controller communicates with the repository for changing the intended investigator or removing the corresponding investigator from its seciton.
	//This controller has the features to add upp the motive for removing the investigator

	[Authorize(Roles = "Manager")]
	public class ManagerController : Controller
	{
        private IHttpContextAccessor contextAcc;
        private readonly IEnviormentRepository repository;

		public ManagerController(IEnviormentRepository repo, IHttpContextAccessor cont)
		{
			repository = repo;
			contextAcc = cont;
		}
	
		


		public ViewResult Manager(string StatusChoice, string EmployeeChoice, string caseNumber)
		{

			var errands = repository.FetchErrandsManager();
			var filteredEmployees = repository.FetchEmployeeByManagerDepartment();
        if (!string.IsNullOrEmpty(caseNumber))
            {
                errands = errands.Where(e => e.RefNumber.ToString() == caseNumber);
            }
		else 
		{ 
            if (StatusChoice == null && EmployeeChoice == null)
			{
				var firstViewModel = new ManagerViewModel
				{
					Errands = errands,
					ErrandStatuses = repository.ErrandStatuses,
					Employees = filteredEmployees
				};

				return View(firstViewModel);
			}
			if (StatusChoice != "Välj alla" && EmployeeChoice == "Välj alla")
			{
				errands = errands.Where(e => e.StatusName == StatusChoice && e.EmployeeName == e.EmployeeName);
			}
			if (StatusChoice == "Välj alla" && EmployeeChoice != "Välj alla")
			{
				errands = errands.Where(e => e.StatusName == e.StatusName && e.EmployeeName == EmployeeChoice);
			}
			if (StatusChoice != "Välj alla" && EmployeeChoice != "Välj alla")
			{
				errands = errands.Where(e => e.StatusName == StatusChoice && e.EmployeeName == EmployeeChoice);
			}
            }
            var secondViewModel = new ManagerViewModel
			{
				Errands = errands.ToList(),
				ErrandStatuses = repository.ErrandStatuses,
				Employees = filteredEmployees
			};
            

            return View(secondViewModel);
		}

		public ViewResult CrimeManager(int errandId)
		{
			var errands = repository.FetchErrandsManager();
			var filteredEmployees = repository.FetchEmployeeByManagerDepartment();

			ViewBag.Id = errandId;
			TempData["Id"] = errandId;
			var thirdViewModel = new ManagerViewModel
			{
				Errands = errands,
				ErrandStatuses = repository.ErrandStatuses,
				Employees = filteredEmployees,


			};
			return View(thirdViewModel);
		}


		public IActionResult SavedManager(string ChosenManager, bool noAction, string reason)
		{
			int Id = (int)TempData["Id"];

			if (ChosenManager == "Välj" && noAction == false)
			{
				return RedirectToAction("CrimeManager", new { errandId = Id });


			}
			repository.UpdateErrandToManager(Id, ChosenManager, noAction, reason);
			return RedirectToAction("Manager");
			}
		}

			


		
}
