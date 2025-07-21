using Newtonsoft.Json;
using Sif;
using Sif.Customers;
using Sif.Data;
using Sif.Journal;
using Sif.Security;
using Sif.Services;

namespace CAP.Courses
{
	public class GetCompletedPendingCoursesData : DataService
	{
		public GetCompletedPendingCoursesData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCompleted, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserLogOnName, this.Dictionary.Security.UserLogOn);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "AssignedCourses", "AssignedCourse", true);
					
				state = ServiceState.Accepted;
			}
			return state;
		}

		private static readonly String fCompleted = "SELECT COUNT(1) TOTAL, state FROM CAP.assigned_courses AC " +
			"JOIN CAP.Access_users AU ON AC.USER_ID = AU.USERID " +
			"WHERE username = " + DataDictSecurity.ParUserLogOn + 
			" GROUP BY STATE";
	}
}
