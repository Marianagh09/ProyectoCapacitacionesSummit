using Newtonsoft.Json;

namespace CAP.Courses
{
	public class AssignedCourseItem
	{
		[JsonProperty("TOTAL")]
		public double Total { get; set; }

		[JsonProperty("STATE")]
		public double State { get; set; }
	}

	public class AssignedCourse
	{
		[JsonProperty("AssignedCourse")]
		public List<AssignedCourseItem> ListAssignedCourses { get; set; }
	}
}
