using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.Security.Roles;
using Sif.Services;

namespace CAP.Quiz
{
	public class GetResultsData : DataService
	{
		public GetResultsData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fUser, this.Connection))
			{
				command.AddParameter(this.Dictionary.Roles, DataDictRoles.DestinationRoleIdName, this.Dictionary.Roles.RoleId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);
				state = ServiceState.Accepted; 
			}
			return state;
		}

		private static readonly String fUser = ""; 
	}
}
