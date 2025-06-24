using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using CAP.Courses;

namespace ProyectoCapacitacionesSummit.Controllers
{
	public class CoursesController : SifControllerBase
	{
		[HttpGet (Name = "GetCourses")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetCourses(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetCoursesBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet (Name = "GetInfoByCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetInfoByCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetInfoByCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost (Name = "NewCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPut (Name = "UpdateCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutUpdateCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PutUpdateCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpDelete (Name = "DeleteCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult DeleteCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new DeleteCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost (Name = "AssignedCourse")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostAssignedCourse(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostAssignedCourseBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPut (Name = "EndModule")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutEndModule(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PutEndModuleBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

	}
}
