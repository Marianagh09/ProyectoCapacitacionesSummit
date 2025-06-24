using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.CustomerApplications;
using Sif.Data;
using Sif.Services;

namespace CAP.Quiz
{
	public class GetInfoQuizData : DataService
	{
		public GetInfoQuizData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fInfoQuiz, this.Connection))
			{
				command.AddParameter(this.Dictionary.CustomerApplications, DataDictCustomerApplications.ApplicationIdName, this.Dictionary.CustomerApplications.ApplicationId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);
				state = ServiceState.Accepted;
			}
			return state;
		}

		private static readonly String fInfoQuiz = "SELECT * FROM CAP.Quizzes WHERE quizzId = " + DataDictCustomerApplications.ParApplicationId; 
	}
}
