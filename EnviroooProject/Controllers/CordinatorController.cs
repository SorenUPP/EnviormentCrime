using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EnviroooProject.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EnviroooProject.Controllers
{
	//this class takes responsibility for the cordinator related section of the website which takes place as the main admin which have access more than any other admin role.
	//this class handles the data changes, transers and developments och website's database.

	[Authorize(Roles = "Cordinator")]
	public class CordinatorController : Controller
	{
		
		private readonly IEnviormentRepository repository;

		public CordinatorController(IEnviormentRepository repo)
		{
			repository = repo;
			
		}

        public ViewResult StartCordinator(string StatusChoice, string DepartmentChoice, string caseNumber)
        {
            var errandList = repository.FetchErrandList();
			
		if (!string.IsNullOrEmpty(caseNumber))
			{
				errandList = errandList.Where(e => e.RefNumber.ToString() == caseNumber);
			}
		else { 
				if (StatusChoice == null && DepartmentChoice == null)	
				{
					ViewBag.ErrandList = errandList.ToList();
					return View(repository);
				}
			
				if (StatusChoice != "Välj alla" && DepartmentChoice == "Välj alla")
				{
					errandList = errandList.Where(e => e.StatusName == StatusChoice);
				}
				if (StatusChoice == "Välj alla" && DepartmentChoice != "Välj alla")
				{
					errandList = errandList.Where(e => e.StatusName == e.StatusName && e.DepartmentName == DepartmentChoice );
				}
				if (StatusChoice != "Välj alla" && DepartmentChoice != "Välj alla")
				{
					errandList = errandList.Where(e => e.StatusName == StatusChoice && e.DepartmentName == DepartmentChoice);
				}

        }

            ViewBag.ErrandList = errandList.ToList();
            return View(repository);
        }


        //Corresponding method to send the listed data to the viewcomponent of Crime-related views
        public ViewResult CrimeCordinator(int errandId)
		{

			ViewBag.Id = errandId;
			TempData["Id"] = errandId;
			return View(repository.Departments);
		}



		public ViewResult ReportCrime()
		{
			var myErrand = HttpContext.Session.Get<Errand>("NewErrand");
			if (myErrand == null)
			{
				return View();
			}
			else
			{
				return View(myErrand);
			}
		}

		public ViewResult Thanks(int errandId)
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrand");
			if (errand != null)
			{
				repository.SaveErrand(errand);

				ViewBag.RefNumber = errand.RefNumber;

				HttpContext.Session.Remove("NewErrand");
			}
			ViewBag.Id = errandId;
			return View();
		}


		//To send the filled from from reportcrime to Validate view of cordinator
		public ViewResult Validate(Errand Errand)
		{

			HttpContext.Session.Set<Errand>("NewErrand", Errand);
			return View(Errand);
		}
		public IActionResult SaveDepartment(string ChosenDepartment)
		{
			//hämta från tempdata
			int Id = (int)TempData["Id"];
			
			if(ChosenDepartment == "D00" || ChosenDepartment == "Välj")
			{
				
				
				return RedirectToAction("CrimeCordinator", new {errandId = Id});

			}

			repository.UpdateErrandToDepartment(Id, ChosenDepartment);

			return RedirectToAction("StartCordinator");
		}
		
	}
}
