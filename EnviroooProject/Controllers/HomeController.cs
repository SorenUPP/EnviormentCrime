using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EnviroooProject.Infrastructure;

namespace EnviroooProject.Controllers
{
	public class HomeController : Controller
	{
		//This controller handles the viewing section of the website besides creating the form object which users will fill for the sake of reporting crime related accidents
		public ViewResult Index()
		{
			var myErrand = HttpContext.Session.Get<Errand>("CitizenErrand");
			if (myErrand == null)
			{
				return View();
			}
			else
			{
				return View(myErrand);
			}

		}
		
	}

}
