using Microsoft.AspNetCore.Mvc;
using EnviroooProject.Models;

namespace EnviroooProject.Components
{
	// The responding viewcomponent that takes responsibility to transfer data to the viewcomponent razorview which is shared between several classes
	//this class takes responsibility for the crime-views database linked data which is shared between all crime-views.

	public class ErrandDetailViewComponent : ViewComponent
	{
		private readonly IEnviormentRepository repository;

		public ErrandDetailViewComponent(IEnviormentRepository repo)
		{
			repository = repo;
		}

		public IViewComponentResult Invoke(int id)
		{
			var errand = repository.GetErrandDetail(id);

            return View("ErrandDetail", errand);
		}

	}
}
