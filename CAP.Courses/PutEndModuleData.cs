using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Journal;
using Sif.Services;

namespace CAP.Courses
{
	public class PutEndModuleData : DataService
	{
		public PutEndModuleData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fUpdate, this.Connection))
			{
				command.AddParameter(this.Dictionary.Journal, DataDictJournal.LineStatusName, this.Dictionary.Journal.LineStatus);
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.FileIdName, this.Dictionary.ImEx.FieldId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}
			return state;
		}

		private static readonly String fUpdate = ""; 
	}
}
