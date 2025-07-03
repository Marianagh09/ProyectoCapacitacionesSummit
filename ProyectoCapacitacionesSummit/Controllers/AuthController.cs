using System.Net.Mime;
using System.Text.Json;
using CAP.Auth;
using Microsoft.AspNetCore.Mvc;
using ProyectoCapacitacionesSummit.Models;
using Sif;
using Sif.Rest.Api;
using Sif.Services;

namespace ProyectoCapacitacionesSummit.Controllers
{
	[Route("users")]
	public class AuthController : SifControllerBase
	{

		[HttpGet("GetUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetUserData(Int32 userId)
		{
			this.Dictionary.Security.UserId = userId; 
			_ = this.StartService(new GetUserBusiness(this.Dictionary));
			return this.Ok();
		}

		[HttpPost("PostAuthentication")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostAuth(User user)
		{
			this.Dictionary.Security.UserLogOn = user.UserName;
			this.Dictionary.Security.RawPassword = user.Password;
			_ = this.StartService(new LogOnBusiness(this.Dictionary));
			//invocar la clase quue genera el jwt 
			return this.Ok(new SifWebResponse { JsonResponseObject = JsonSerializer.Deserialize<object>(this.Dictionary.Sif.JsonResponseObject)});
			//return this.Ok(new SifResponseDto {JsonResponseObject = this.SifResponse.JsonResponseObject, Message = this.SifResponse.Message});
		}


		[HttpPost("PostNewUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewUser(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}
	}
}
