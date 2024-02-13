using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using T119_AzureAppService.Data;
using T119_AzureAppService.Dtos;
using T119_AzureAppService.Helpers;
using T119_AzureAppService.Models;

namespace T119_AzureAppService.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class AuthController(IConfiguration config) : ControllerBase
	{
		private readonly ReusableSql _reusableSql = new(config);
		private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<UserForRegistrationDto, UserModel>();
		}));
		private readonly DataContextDapper _dapper = new(config);
		private readonly AuthHelper _authHelper = new(config);

		[AllowAnonymous]
		[HttpPost("Register")]
		public IActionResult Register(UserForRegistrationDto userForRegistration)
		{
			if (userForRegistration.Password == userForRegistration.PasswordConfirm)
			{
				string sqlCheckUserExists =
					$"SELECT [Email] FROM TutorialAppSchema.Auth WHERE Email = '{userForRegistration.Email}'";

				IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

				if (!existingUsers.Any())
				{
					UserForLoginDto userForSetPassword = new()
					{
						Email = userForRegistration.Email,
						Password = userForRegistration.Password
					};

					if (_authHelper.SetPassword(userForSetPassword))
					{
						UserModel user = _mapper.Map<UserModel>(userForRegistration);
						user.Active = true;

						if (_reusableSql.UpsertUser(user))
						{
							return Ok();
						}

						return StatusCode(400, "Failed to add User.");
					}

					return StatusCode(400, "Failed to register User.");
				}

				return StatusCode(400, "User with this email already exists!");
			}

			return StatusCode(400, "Passwords do not match!");
		}

		[HttpPost("ResetPassword")]
		public IActionResult ResetPassword(UserForLoginDto userForSetPassword)
		{
			if (_authHelper.SetPassword(userForSetPassword))
			{
				return Ok();
			}

			return StatusCode(400, "Failed to update password.");
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		public IActionResult Login(UserForLoginDto userForLogin)
		{
			string sqlForHashAndSalt = @$"
			EXEC TutorialAppSchema.spLoginConfirmation_Get
			@Email = '{userForLogin.Email}'
			";

			DynamicParameters sqlParameters = new();
			sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);

			UserForLoginConfirmationDto userForLoginConfirmation =
				_dapper.LoadDataSingleWithParameters<UserForLoginConfirmationDto>
				(
					sqlForHashAndSalt,
					sqlParameters
				);


			byte[] passwordHash =
				_authHelper.GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);

			for (int i = 0; i < passwordHash.Length; i++)
			{
				if (passwordHash[i] != userForLoginConfirmation.PasswordHash[i])
				{
					return StatusCode(401, "Incorrect password!");
				}
			}

			string sqlUserId = @$"
			SELECT
				[UserId]
			FROM TutorialAppSchema.Users 
			WHERE Email = '{userForLogin.Email}'
			";

			int userId = _dapper.LoadDataSingle<int>(sqlUserId);

			return Ok(new Dictionary<string, string> { { "token", _authHelper.CreateToken(userId) } });
		}

		[HttpGet("RefreshToken")]
		public string RefreshToken()
		{
			string sqlUserId = @$"
			SELECT
				[UserId]
			FROM TutorialAppSchema.Users 
			WHERE UserId = '{User.FindFirst("userId")?.Value}'
			";

			int userId = _dapper.LoadDataSingle<int>(sqlUserId);

			return _authHelper.CreateToken(userId);
		}
	}
}
