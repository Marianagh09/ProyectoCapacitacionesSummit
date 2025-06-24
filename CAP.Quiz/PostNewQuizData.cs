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
	public class PostNewQuizData : DataService
	{
		public PostNewQuizData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fPost, this.Connection))
			{
				command.AddParameter(this.Dictionary.CustomerApplications, DataDictCustomerApplications.NameName, this.Dictionary.CustomerApplications.Name);
				command.AddParameter(this.Dictionary.CustomerApplications, DataDictCustomerApplications.ServiceIdName, this.Dictionary.CustomerApplications.ServiceId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fPost = "INSER INTO CAP.Quizzes (title, module_Id) VALUES ("+ DataDictCustomerApplications.ParName + "," +
			DataDictCustomerApplications.ParServiceId +")"; 
	}
}
