using System.Collections.Generic;
using System.Net.Mime;
using CAP.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;

namespace ProyectoCapacitacionesSummit.Controllers
{
	public class AuthController : SifControllerBase
	{

		

		[HttpGet (Name = "GetUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetUserData(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost (Name = "PostAuthentication")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostAuth(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new LogOnBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}


		[HttpPost (Name = "PostNewUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public  IActionResult PostNewUser(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}
	}
}
