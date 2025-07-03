using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCapacitacionesSummit.Models;
using Sif;
using Sif.Data;
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

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurseUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Roles, DataDictRoles.DestinationRoleIdName, this.Dictionary.Roles.DestinationRoleId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);

				state = ServiceState.Accepted;
			}
			return state;
		}

		private static readonly String fCurseUser = "SELECT u.nombre, c.title, a.state, a.assignament_date  FROM CAP.Assigned_courses a " +
			"join CAP.Users u on u.id = a.user_Id " +
			"join CAP.Courses c on c.coursesId = a.course_Id" +
			"WHERE user_Id = " + DataDictRoles.ParDestinationRoleId; 
	}
}
