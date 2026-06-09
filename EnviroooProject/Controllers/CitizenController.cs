using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using EnviroooProject.Infrastructure;

namespace EnviroooProject.Controllers
{
	//responding controller for citizen related views which are responsible for the public related features of the website.
	//this controller takes responsibility for showing the corresponding views of the website, tranfering data between them and in general communicate with the repositoryl
	public class CitizenController : Controller
	{
		private readonly IEnviormentRepository repository;

		public CitizenController(IEnviormentRepository repo)
		{
			repository = repo;
		}
		public ViewResult Contact()
		{
			return View();
		}

		public ViewResult Faq()
		{
			return View();
		}

		public ViewResult Services()
		{
			return View();
		}
		//To send the filled forms data to validate view
		[HttpPost]
		public ViewResult Validate(Errand Errand)
		{

			HttpContext.Session.Set<Errand>("CitizenErrand", Errand);
			return View(Errand);
		}



		public ViewResult Thanks(int errandId)
		{
			
			var errand = HttpContext.Session.Get<Errand>("CitizenErrand");
			if (errand != null)
			{
				repository.SaveErrand(errand);

				ViewBag.RefNumber = errand.RefNumber;

				HttpContext.Session.Remove("CitizenErrand");
			}
			ViewBag.Id = errandId;
			return View();
		}

	}
}
