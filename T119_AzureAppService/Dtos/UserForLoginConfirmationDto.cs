namespace T119_AzureAppService.Dtos
{
	public class UserForLoginConfirmationDto
	{
		public byte[] PasswordHash { get; set; } = [];
		public byte[] PasswordSalt { get; set; } = [];
	}
}
