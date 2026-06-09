using EnviroooProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EnviroooProject.Controllers
{
	//Responding controller which takes responsibility for data transfering and in general controll of account related methods.
	[Authorize]
	public class AccountController : Controller
	{

		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IEnviormentRepository repository;
		public AccountController(IEnviormentRepository repo, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
		{
			repository = repo;
			userManager = userMgr;
			signInManager = signInMgr;
		}

		[AllowAnonymous]

		public ViewResult Login(string returnUrl)
		{
			return View(
				new LoginModel
				{
					ReturnUrl = returnUrl
				}
				);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);
				if (User != null)
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
					{
						if(await userManager.IsInRoleAsync(user, "Cordinator"))
						{
							return Redirect("~/Cordinator/StartCordinator");
						}
						if (await userManager.IsInRoleAsync(user, "Manager"))
						{
							return Redirect("~/Manager/Manager");
						}
						if (await userManager.IsInRoleAsync(user, "Investigator"))
						{
							return Redirect("~/Investigator/Investigator");
						}
					}
				}
			}
			ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
			return View(loginModel);
		}
		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			await signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}

		[AllowAnonymous]
		public ViewResult AccessDenied()
		{
			return View();
		}
	}
}
