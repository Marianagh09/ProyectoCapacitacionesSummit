using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Journal;
using Sif.Security;
using Sif.Security.Roles;
using Sif.Services;

namespace CAP.Courses
{
	public class PostAssignedCourseData : DataService
	{
		public PostAssignedCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()   
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurses, this.Connection))
			{
				  command.AddParameter(this.Dictionary.ImEx, DataDictImEx.JoinIdName, this.Dictionary.ImEx.JoinId);
				  command.AddParameter(this.Dictionary.Journal, DataDictJournal.LineStatusName, this.Dictionary.Journal.LineStatus);
				  command.AddParameter(this.Dictionary.Journal, DataDictJournal.ServiceStartDateTimeName, this.Dictionary.Journal.LineStatus);
				  command.AddParameter(this.Dictionary.Roles, DataDictRoles.DestinationRoleIdName, this.Dictionary.Roles.DestinationRoleId);
				  command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
				return state;
			}
		}

		private static readonly String fCurses = ""; 
	}
}
