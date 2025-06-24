using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using System.Collections.Generic;
using CAP.Users;

namespace ProyectoCapacitacionesSummit.Controllers
{
	public class UsersController : SifControllerBase
	{

		[HttpGet (Name = "Users")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetUsers(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetUsersBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet (Name  = "SpecificUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetByUserId(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetByUserIdBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost (Name = "NewUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewUser(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPut (Name = "UserInfo")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutUserInfo(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PutUserInfoBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		//[HttpDelete (Name = "CourseDelete")]
		//[Consumes(MediaTypeNames.Application.Json)]
		//[Produces(MediaTypeNames.Application.Json)]
		//[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		//public IActionResult DeleteCapacitation(DataDict dictionary)
		//{
		//	this.Dictionary = dictionary;
		//	_ = this.StartService(new DeleteCapacitationBusiness(this.Dictionary));
		//	return this.Ok(this.SifResponse);
		//}

		[HttpGet (Name = "CourseByUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult getCourseByUser(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetCourseByUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet (Name = "Certificate")]
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
