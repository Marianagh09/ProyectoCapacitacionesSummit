using Newtonsoft.Json;
using System.Reflection;

namespace ProyectoCapacitacionesSummit.Models
{
	public class Course
	{
		public Int32 CourseId { get; set; }
		public String? Title { get; set; }
		public String? Description { get; set; }
		public DateTime CreationDate { get; set; }
		public String? CreatorId { get; set; }

		[JsonProperty("modules")]
		public List<Modules> Modules { get; set; }
	}
}
