using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using CAP.Courses;
using Microsoft.AspNetCore.Authorization;
using ProyectoCapacitacionesSummit.Models;
using Newtonsoft.Json;

namespace ProyectoCapacitacionesSummit.Controllers
{
	[Route("Courses")]
	public class CoursesController : SifControllerBase
	{
		[HttpGet ("GetCompletedCourses")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetCourses(String Id)
		{
			this.Dictionary.Security.TellerId = Id;
			_ = this.StartService(new GetCompletedCoursesBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet ("GetPendignCourseUser")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetInfoByCourse(String Id)
		{
			this.Dictionary.Security.TellerId = Id;
			_ = this.StartService(new GetPendignCourseUserBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		//[Authorize]
		[HttpPost ("NewCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewCourse(Course course)
		{
			
			this.Dictionary.Sif.JsonResponseObject = JsonConvert.SerializeObject(course);
			//this.Dictionary.Journal.StartDateTime = course.CreationDate;
			_ = this.StartService(new PostNewCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[Authorize]
		[HttpPut ("UpdateCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutUpdateCourse(Course course)
		{
			this.Dictionary.ImEx.Name = course.Title;
			this.Dictionary.ImEx.Description = course.Description;
			this.Dictionary.Security.TellerId = course.CreatorId;
			this.Dictionary.Journal.StartDateTime = course.CreationDate;
			_ = this.StartService(new PutUpdateCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[Authorize]
		[HttpDelete ("DeleteCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult DeleteCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new DeleteCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[Authorize]
		[HttpPost ("AssignedCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostAssignedCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostAssignedCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet ("GetCourseReader")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutEndModule(Int32 CourseId)
		{
			this.Dictionary.ImEx.FileId = CourseId; 
			_ = this.StartService(new GetCourseReaderBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}


		[HttpGet("GetAssignedCourses")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetAssignedCourses(String userName)
		{
			this.Dictionary.Security.UserLogOn = userName;
			_ = this.StartService(new GetAssignedCoursesByUserIdBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

	}
}
