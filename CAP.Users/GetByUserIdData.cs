using Sif;
using Sif.Data;
using Sif.Security.Roles;
using Sif.Services;

namespace CAP.Users
{
	public class GetByUserIdData : DataService
	{
		public GetByUserIdData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Roles, DataDictRoles.DestinationRoleIdName, this.Dictionary.Roles.DestinationRoleId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);

				state = ServiceState.Accepted;
			}

			return state;
		}

		private static readonly String fUser = "SELECT * FROM CAP.Access_users WHERE userId = " + DataDictRoles.ParDestinationRoleId; 
	}
}
