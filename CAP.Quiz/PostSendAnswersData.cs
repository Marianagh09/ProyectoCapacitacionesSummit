using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.Data;
using Sif.Services;

namespace CAP.Quiz
{
	public class PostSendAnswersData : DataService
	{
		public PostSendAnswersData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fSend, this.Connection))
			{

			}
			return state;
		}

		private static readonly String fSend = ""; 
	}
}
