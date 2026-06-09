using System.ComponentModel.DataAnnotations;

namespace EnviroooProject.Models
{
	public class Errand
	{

		public int ErrandId { get; set; }
		public String RefNumber { get; set; }

		[Display(Name = "Var har brottet skett någonstans?")]
		[Required(ErrorMessage = "Du måste fylla i den här rutan!")]
		public String Place { get; set; }

		[Display(Name = "Vilken typ av brott?")]
		[Required(ErrorMessage = "Du måste fylla i den här rutan!")]
		public String TypeOfCrime { get; set; }

		[Display(Name = "När skedde brottet?")]
		[Required(ErrorMessage = "Du måste fylla i den här rutan!")]
		[DataType(DataType.Date)]
		public DateTime DateOfObservation { get; set; }

		[Display(Name = "Ditt namn (för- och efternamn):")]
		[Required(ErrorMessage = "Du måste fylla i den här rutan!")]
		public String InformerName { get; set; }

		[Required(ErrorMessage = "Du måste fylla i den här rutan!")]
		[RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$", ErrorMessage = "Formatet är 0XXX-XXXXXXX ")]
		[Display(Name = "Din telefon: ")]
		public String InformerPhone { get; set; }

		[Display(Name = "Beskriv din observation (ex. namn på misstänkt person)")]
		public String Observation { get; set; }

		public String InvestigatorInfo { get; set; }
		public String InvestigatorAction { get; set; }
		public String StatusId { get; set; }
		public String DepartmentId { get; set; }
		public String EmployeeId { get; set; }
		
		public ICollection<Sample> Samples { get; set; }
		public ICollection<Picture> Pictures { get; set; }

	
	}
}
