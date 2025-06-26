using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using CAP.Quiz;

namespace ProyectoCapacitacionesSummit.Controllers
{
	[Route("quiz")]
	public class QuizController : SifControllerBase
	{
		[HttpGet ("InfoQuiz")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetInfoQuiz(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetInfoQuizBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost ("NewQuiz")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostNewQuiz(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostNewQuizBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPut ("UpdateQuiz")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PutUpdateQuiz(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PutUpdateQuizBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpDelete ("DeleteQuiz")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult DeleteQuiz(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new DeleteQuizBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpPost ("SendAnswers")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult PostSendAnswers(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new PostSendAnswersBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}

		[HttpGet ("GetResult (para usuario o todos)")]
		[Consumes(MediaTypeNames.Application.Json)]
		[Produces(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SifWebResponse), StatusCodes.Status200OK)]
		public IActionResult GetResults(DataDict dictionary)
		{
			this.Dictionary = dictionary;
			_ = this.StartService(new GetResultsBusiness(this.Dictionary));
			return this.Ok(this.SifResponse);
		}
	}
}
