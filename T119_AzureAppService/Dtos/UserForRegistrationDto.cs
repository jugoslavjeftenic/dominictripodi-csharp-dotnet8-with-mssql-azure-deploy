﻿namespace T119_AzureAppService.Dtos
{
	public class UserForRegistrationDto
	{
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string Email { get; set; } = "";
		public string Gender { get; set; } = "";
		public string Password { get; set; } = "";
		public string PasswordConfirm { get; set; } = "";
		public string JobTitle { get; set; } = "";
		public string Department { get; set; } = "";
		public decimal Salary { get; set; }
	}
}
