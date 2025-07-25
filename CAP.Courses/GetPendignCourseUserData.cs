using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Security;
using Sif.Services;

namespace CAP.Courses
{
	public class GetPendignCourseUserData : DataService
	{
		public GetPendignCourseUserData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurses, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "Courses", "Course", true);
				state = ServiceState.Accepted;
			}

			return state;
		}

		private static readonly String fCurses = "SELECT " +
			"  c.title," +
			" c.description," +
			" a.name  creator_name, " +
			" c.COURSESID " +
			"FROM CAP.Courses c " +
			"JOIN CAP.Access_users a ON a.USERID = c.CREATOR_ID " +
			"JOIN CAP.Assigned_courses ass ON ass.COURSE_ID = c.COURSESID " +
			"WHERE ass.state = 0   AND ass.USER_ID =  " + DataDictSecurity.ParTellerId;
	}
}
