using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.Data;
using Sif.Security;
using Sif.Security.Roles;
using Sif.Services;

namespace CAP.Users
{
	public class PutUserInfoData : DataService
	{
		public PutUserInfoData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fNewInfoUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.NewFirstNameName, this.Dictionary.Security.NewFirstName);
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserNameName, this.Dictionary.Security.UserName);
				command.AddParameter(this.Dictionary.Roles, DataDictRoles.RoleIdName, this.Dictionary.Roles.RoleId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fNewInfoUser = "UPDATE CAP.Access_users SET nombre = " + DataDictSecurity.ParNewFirstName +
			", email = " + DataDictSecurity.ParUserName + ", rol_Id = " + DataDictRoles.ParRoleId + "WHERE userId = " +
			DataDictSecurity.ParTellerId; 
	}
}
