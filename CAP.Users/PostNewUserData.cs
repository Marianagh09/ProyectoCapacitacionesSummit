using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.Data;
using Sif.Security.Roles;
using Sif.Security;
using Sif.Services;

namespace CAP.Users
{
	public class PostNewUserData : DataService
	{
		public PostNewUserData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fNewUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.NewFirstNameName, this.Dictionary.Security.NewFirstName);
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserNameName, this.Dictionary.Security.UserName);
				command.AddParameter(this.Dictionary.Security, DataDictRoles.RoleIdName, this.Dictionary.Security.ActiveUserRoleId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fNewUser = "INSERT INTO CAP.Access_users (name, email, rol_Id) VALUES (" + DataDictSecurity.ParNewFirstName + "," +
		  	DataDictSecurity.ParUserName + "," + DataDictRoles.ParRoleId + ")";
	}
}
