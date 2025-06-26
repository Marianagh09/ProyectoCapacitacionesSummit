using System.Net.Mime;
using CAP.Auth;
using Microsoft.AspNetCore.Mvc;
using ProyectoCapacitacionesSummit.Models;
using Sif;
using Sif.Rest.Api;

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
			this.Dictionary.Security.UserLogOn = user.Email;
			this.Dictionary.Security.RawPassword = user.Name;
			_ = this.StartService(new LogOnBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
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
