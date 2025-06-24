using Microsoft.AspNetCore.Mvc;

namespace ProyectoCapacitacionesSummit.Controllers
{
	public class NotificationsController : ControllerBase 
	{

		[HttpGet (Name = "")]
		public IEnumerable<Task> Get()
		{
			return new Task[0];
		}
	}
}
