using System.ComponentModel.DataAnnotations;

namespace EnviroooProject.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Vänligen fyll i användarnamn")]
		[Display(Name = "Användarnamn")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Vänligen fyll i lösenord")]
		[Display(Name = "Lösenord")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
