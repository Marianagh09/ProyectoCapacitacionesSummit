using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class GetAssignedCoursesByUserIdBusiness : BusinessService
	{
		public GetAssignedCoursesByUserIdBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{

			ServiceState state = this.StartService(new GetAssignedCoursesByUserIdData(this.Dictionary));
			if (state == ServiceState.Accepted)
			{
				state = this.StartService(new GetCompletedPendingCoursesData(this.Dictionary));

				AssignedCourse assignedCourse = JsonConvert.DeserializeObject<AssignedCourse>(this.Dictionary.Sif.JsonResponseObject);
				if (assignedCourse != null)
				{
					assignedCourse.ListAssignedCourses.Add(new AssignedCourseItem()
					{
						State = -1,
						Total = this.Dictionary.Currency.CurrencyType
					});
				}

				this.Dictionary.Sif.JsonResponseObject = JsonConvert.SerializeObject(assignedCourse);


			}


			return state;
		}

	}
}
