using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCapacitacionesSummit.Models;
using Sif;
using Sif.Data;
using Sif.Security;
using Sif.Security.Roles;
using Sif.Services;

namespace CAP.Users
{
	public class GetCourseByUserData : DataService
	{
		public GetCourseByUserData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (
				SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurseUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
				//command.AddParameter(this.Dictionary.Roles, DataDictRoles.DestinationRoleIdName, this.Dictionary.Roles.DestinationRoleId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "Courses", "Course", true);

				state = ServiceState.Accepted;
			}
			return state;
		}

		//		private static readonly String fCurseUser = "SELECT u.name, c.title, a.state, a.assignament_date  FROM CAP.Assigned_courses a " + 
		//"join CAP.Access_users u on u.userid = a.user_Id "+ 
		//"join CAP.Courses c on c.coursesId = a.course_Id " +
		//"WHERE u.userId = " + DataDictRoles.ParDestinationRoleId; 

		//private static readonly String fCurseUser = "select c.title, c.description, a.name from CAP.Courses  c" +
		//	"join CAP.Access_users  a on a.userid =" + DataDictSecurity.ParTellerId;

		private static readonly String fCurseUser = @"select c.title, c.description, a.name, c.CoursesId from CAP.Courses c 
			join CAP.Access_users  a on a.userid = c.creator_id
			where Creator_id = " + DataDictSecurity.ParTellerId;
	}
}
