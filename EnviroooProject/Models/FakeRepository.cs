using EnviroooProject.Models;

using System;

namespace EnviroooProject.Models
{



	public class FakeRepository
	{
		public IQueryable<Department> Departments => new List<Department>
			{
			new Department { DepartmentId = "D00" , DepartmentName = "Småstads Kommun"},
			new Department { DepartmentId = "D01" , DepartmentName = "IT-avdelningen"},
			new Department { DepartmentId = "D02" , DepartmentName = "Lek och skoj"},
			new Department { DepartmentId = "D03" , DepartmentName = "Miljöskydd"}


			}.AsQueryable();
		public IQueryable<ErrandStatus> ErrandStatuses => new List<ErrandStatus>
			{
			new ErrandStatus {StatusId= "S_A", StatusName="Rapporterad"},
			new ErrandStatus {StatusId= "S_B", StatusName="Ingen åtgärd"},
			new ErrandStatus {StatusId="S_C", StatusName="Startad"},
			new ErrandStatus {StatusId="S_D", StatusName="Färdig"}
			}.AsQueryable();

		public IQueryable<Employee> Employees => new List<Employee>
			{
			new Employee{EmployeeId = "E302", EmployeeName = "Martin Bäck", RoleTitle = "investigator", DepartmentId ="D01"},
			new Employee{EmployeeId = "E301", EmployeeName = "Lena Kristersson", RoleTitle = "investigator", DepartmentId ="D01"},
			new Employee{EmployeeId = "E401", EmployeeName = "Oskar Jansson", RoleTitle = "investigator", DepartmentId ="D02"},
			new Employee{EmployeeId = "E501", EmployeeName = "Susanne Strid", RoleTitle = "investigator", DepartmentId ="D03"},

			}.AsQueryable();
		public IQueryable<Errand> Errands => new List<Errand>
			{
			new Errand{
			RefNumber = "2023-45-0001",
			Place = "Skogslunden vid Jensens gård",
			TypeOfCrime="Sopor",
			DateOfObservation = new DateTime(2023,04,24),
			Observation ="Anmälaren var på promeand i skogslunden när hon upptäckte soporna",
			InvestigatorInfo = "Undersökning har gjorts och bland soporna hittades bl.a ett brev till Gösta Olsson",
			InvestigatorAction = "Brev har skickats till Gösta Olsson om soporna och anmälan har gjorts till polisen 2023-05-01",
			InformerName = "Ada  Bengtsson",
			InformerPhone = "0432-5545522",
			StatusId="Klar",
			DepartmentId="Renhållning och avfall",
			EmployeeId ="Susanne Strid"},

			new Errand{
			RefNumber = "2023-45-0002",
			Place = "Småstadsjön",
			TypeOfCrime="Oljeutsläpp",
			DateOfObservation = new DateTime(2023,04,29),
			Observation ="Jag såg en oljefläck på vattnet när jag var där för att fiska",
			InvestigatorInfo = "Undersökning har gjorts på plats, ingen fläck har hittas",
			InvestigatorAction = "",
			InformerName = "Bengt Svensson",
			InformerPhone = "0432-5152255",
			StatusId="Ingen åtgärd",
			DepartmentId="Natur och Skogsvård",
			EmployeeId="Oskar Jansson"},

			new Errand{
			RefNumber = "2023-45-0003",
			Place = "Ödehuset",
			TypeOfCrime="Skrot",
			DateOfObservation = new DateTime(2023,05,02),
			Observation ="Anmälaren körde förbi ödehuset och upptäcker ett antal bilar och annat skrot",
			InvestigatorInfo ="Undersökning har gjorts och bilder har tagits",
			 InvestigatorAction = "",
			InformerName = "Olle Pettersson",
			InformerPhone = "0432-5255522",
			StatusId="Påbörjad",
			DepartmentId="Miljö och Hälsoskydd",
			EmployeeId ="LenaKristersson"},

			new Errand{
			RefNumber = "2023-45-0004",
			Place = "Restaurang Krögaren",
			TypeOfCrime="Buller",
			DateOfObservation = new DateTime(2023,06,04),
			Observation="Restaurangen hade för högt ljud på så man inte kunde sova",
			InvestigatorInfo ="Bullermätning har gjorts. Man håller sig inom riktvärden",
			InvestigatorAction ="Meddelat restaurangen att tänka på ljudet i fortsättning",
			InformerName = "Roland Jönsson",
			InformerPhone = "0432-5322255",
			StatusId="Klar",
			DepartmentId="Miljö ochHälsokydd",
			EmployeeId ="Martin Bäck"},

			new Errand{
			RefNumber = "2023-45-0005",
			Place = "Torget",
			TypeOfCrime="Klotter",
			DateOfObservation = new DateTime(2023,07,10),
			Observation ="Samtliga skräpkorgaroch bänkar är nedklottrade",
			InvestigatorInfo = "",
			InvestigatorAction = "",
			InformerName = "Peter Svensson",
			InformerPhone = "0432-5322555",
			StatusId="Inrapporterad",
			DepartmentId="Ej tillsatt",
			EmployeeId ="Ej tillsatt"}


			}.AsQueryable();





	}
}


