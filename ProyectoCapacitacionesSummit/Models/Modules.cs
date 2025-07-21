using Newtonsoft.Json;

namespace ProyectoCapacitacionesSummit.Models
{
	public class Modules
	{
		[JsonProperty("courseTitle")]
		public string CourseTitle { get; set; }

		[JsonProperty("courseDescription")]
		public string CourseDescription { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("contentType")]
		public string ContentType { get; set; }

		[JsonProperty("fileUrl")]
		public string FileUrl { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
