using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.CustomerApplications;
using Sif.Data;
using Sif.Services;

namespace CAP.Quiz
{
	public class DeleteQuizData : DataService
	{
		public DeleteQuizData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fDelete, this.Connection))
			{
				command.AddParameter(this.Dictionary.CustomerApplications, DataDictCustomerApplications.ApplicationIdName, this.Dictionary.CustomerApplications.ApplicationId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fDelete = "DELETE * FROM CAP.Quizzes WHERE quizzId = " + DataDictCustomerApplications.ParApplicationId; 
	}
}
