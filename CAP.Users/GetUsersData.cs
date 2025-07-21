using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.Enterprises;
using Sif.Services;

namespace CAP.Users
{
	public class GetUsersData : DataService
	{
		public GetUsersData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fUsers, this.Connection))
			{
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "USERS", "USER", true);

				state = ServiceState.Accepted;
			}
			return state;
		}

		private static readonly String fUsers = "SELECT name FROM CAP.Access_users"; 
	}
}
