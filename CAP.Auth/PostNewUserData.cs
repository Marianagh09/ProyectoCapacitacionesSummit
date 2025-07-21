using Sif;
using Sif.Data;
using Sif.Security;
using Sif.Services;

namespace CAP.Auth
{
	internal class PostNewUserData : DataService
	{
		public PostNewUserData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			Int32 existUser = 0; 

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fConsulta, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserNameName, this.Dictionary.Security.UserName);
				Object result = command.ExecuteScalar(this.Message);
				if (result != null && Convert.ToInt32(result) > 0)
				{
					existUser = 1; 
				}
			}

			if (existUser  == 0)
			{
				using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCreate, this.Connection))
				{
					command.AddParameter(this.Dictionary.Security, DataDictSecurity.NewFirstNameName, this.Dictionary.Security.NewFirstName);
					command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserNameName, this.Dictionary.Security.UserName);
					command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserLogOnName, this.Dictionary.Sif.LoggedUser);
						
					//command.AddParameter(this.Dictionary.Roles, DataDictRoles.RoleIdName, this.Dictionary.Roles.RoleId);
					Int32 rows = command.ExecuteNonQuery(this.Message);
					if (rows > 0)
					{
						state = ServiceState.Accepted;
					}
				}
				return state;
			}
			else
			{
				state = ServiceState.Accepted;
			}

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fId, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserNameName, this.Dictionary.Security.UserName);
				Object result = command.ExecuteScalar(this.Message);
				if (result != null)
				{
					this.Dictionary.Security.TellerId = Convert.ToString(result);
					state = ServiceState.Accepted;
				}
			}
				return state; 
		}



		private static readonly String fConsulta = " SELECT COUNT(1) FROM CAP.Access_users WHERE email = " + DataDictSecurity.ParUserName;

		private static readonly String fCreate = "INSERT INTO CAP.Access_users (name, email, rol_Id, USERNAME) VALUES (" + DataDictSecurity.ParNewFirstName + ",  " +
		  	DataDictSecurity.ParUserName + "," + 1 + ", " + DataDictSecurity.ParUserLogOn +")";

		private static readonly String fId = "select USERID from CAP.Access_users where EMAIL = " + DataDictSecurity.ParUserName;
	}
}






