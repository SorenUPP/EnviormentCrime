using Microsoft.AspNetCore.Mvc;
using EnviroooProject.Models;

namespace EnviroooProject.Components
{

	// The responding viewcomponent that takes responsibility to transfer data to the viewcomponent razorview which is shared between several classes
	//This class takes responsibility for the thanks message in the thanks view
	public class ThanksViewComponent : ViewComponent
	{
		private readonly IEnviormentRepository repository;

		public ThanksViewComponent(IEnviormentRepository repo)
		{
			repository = repo;
		}

		public IViewComponentResult Invoke()
		{
		

			return View("Thanks");
		}

	}
}
