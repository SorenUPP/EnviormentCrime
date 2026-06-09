using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;


namespace EnviroooProject.Controllers
{ 
	//Investigator related section of the website which handles any matter related to investigator admin. Investigators handle the uploading / information add upps of the admin roles.
	//This class communicates directly with the repository for development / savings of files.

	[Authorize(Roles = "Investigator")]
	public class InvestigatorController : Controller
	{

		private readonly IEnviormentRepository repository;
		private IWebHostEnvironment enviorment;

		public InvestigatorController(IEnviormentRepository repo, IWebHostEnvironment env)
		{
			repository = repo;

			enviorment = env;
		}


		public ViewResult Investigator(string StatusChoice, string caseNumber)
	{

			var errands = repository.FetchErrandsInvestigator();
        if (!string.IsNullOrEmpty(caseNumber))
            {
                errands = errands.Where(e => e.RefNumber.ToString() == caseNumber);
            }
        else { 
			if (StatusChoice == null)
			{
				var firstViewModel = new ManagerViewModel
				{
					Errands = errands,
					ErrandStatuses = repository.ErrandStatuses,
					
				};

				return View(firstViewModel);
			}
			if (StatusChoice != "Välj alla")
			{
				errands = errands.Where(e => e.StatusName == StatusChoice);
			}
        }

            var viewModel = new ManagerViewModel
			{
				Errands = errands,
				ErrandStatuses = repository.ErrandStatuses,
				
			};

			return View(viewModel);
	}



		public ViewResult CrimeInvestigator(int errandId)
		{

			ViewBag.Id = errandId;
			TempData["Id"] = errandId;
			return View(repository.ErrandStatuses);
		}

		public async Task<IActionResult> SavedInvestigator(string ChosenStatus, string information, string events, IFormFile loadSample, IFormFile loadImage)
		{
			int Id = (int)TempData["Id"];

			if (information != null || events != null || ChosenStatus != "Välj")
			{
                repository.UpdateErrandToInvestigator(Id, ChosenStatus, information, events);
            }
			

            if (loadSample != null)
			{
				await SaveFile(Id, loadSample, "Samples");
			}
				
			if(loadSample != null) 
			{ 
				await SaveFile(Id, loadImage, "Pictures");
			}
           

            return RedirectToAction("Investigator");
           
        }
		

		private async Task SaveFile(int Id, IFormFile documents, string subfile)
		{
           

            var tempPath = Path.GetTempFileName();

			if (documents.Length > 0)
				{
				using (var stream = new FileStream(tempPath, FileMode.Create))
				{
					await documents.CopyToAsync(stream);
				}
			}

			//unique
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + documents.FileName;

			//Skapa ny sökväg
			var path = Path.Combine(enviorment.WebRootPath, subfile, uniqueFileName);

			//flytta den temporära filen rätt
			System.IO.File.Move(tempPath, path);

            repository.UpdateFolderFiles(Id, uniqueFileName, subfile);

        }

	}
}

