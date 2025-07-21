using Sif;
using Sif.Customers;
using Sif.Data;
using Sif.Security;
using Sif.Services;

namespace CAP.Courses
{
	public class GetAssignedCoursesByUserIdData : DataService
	{
		public GetAssignedCoursesByUserIdData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fConsulta, this.Connection))
			{
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.UserLogOnName, this.Dictionary.Security.UserLogOn);
				Object result = command.ExecuteScalar(this.Message);
				if (result != null)
				{
					this.Dictionary.Currency.CurrencyType = Convert.ToInt32(result);
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fConsulta = "SELECT COUNT(1) TOTAL FROM CAP.assigned_courses AC " +
		"JOIN CAP.Access_users AU ON AC.USER_ID = AU.USERID " +
		"WHERE AU.username = " + DataDictSecurity.ParUserLogOn;
	}
}
