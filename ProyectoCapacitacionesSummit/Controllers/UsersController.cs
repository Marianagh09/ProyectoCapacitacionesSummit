using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using System.Collections.Generic;
using CAP.Users;
using ProyectoCapacitacionesSummit.Models;

namespace ProyectoCapacitacionesSummit.Controllers
{
	[Route("user")]
	public class UsersController : SifControllerBase
	{

		[HttpGet ("Users")]
		//[Consumes(MediaTypeNames.Application.Json)]
		//[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetUsers()
		{
			_ = this.StartService(new GetUsersBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet ("SpecificUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetByUserId(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetByUserIdBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost ("NewUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewUser(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPut ("UserInfo")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutUserInfo(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PutUserInfoBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		//[HttpDelete ("CourseDelete")]
		//[Consumes(MediaTypeNames.Application.Json)]
		//[Produces(MediaTypeNames.Application.Json)]
		//[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		//public IActionResult DeleteCapacitation(DataDict dictionary)
		//{
		//	this.Dictionary = dictionary;
		//	_ = this.StartService(new DeleteCapacitationBusiness(this.Dictionary));
		//	return this.Ok(this.SifResponse);
		//}

		[HttpGet ("CourseByUser")]
		//[Consumes(MediaTypeNames.Application.Json)]
		//[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult getCourseByUser(String Id)
		{
			this.Dictionary.Security.TellerId = Id;
			_ = this.StartService(new GetCourseByUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet ("Certificate")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetCertificate(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetCertificateBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}
	}
}
