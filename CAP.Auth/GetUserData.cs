using Sif;
using Sif.Data;
using Sif.Security;
using Sif.Services;

namespace CAP.Auth
{
	public class GetUserData : DataService
	{
		public GetUserData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fRegistros, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserIdName, this.Dictionary.Security.UserId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);

				state = ServiceState.Accepted;
			}
			return state;
		}


		private static readonly String fRegistros = "Select * from CAP.Access_users where userId = " + DataDictSecurity.ParUserId;
	}
}
