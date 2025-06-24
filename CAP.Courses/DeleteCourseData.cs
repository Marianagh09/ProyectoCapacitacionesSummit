using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Services;

namespace CAP.Courses
{
	public class DeleteCourseData : DataService
	{
		public DeleteCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fDelete, this.Connection))
			{
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.FileIdName, this.Dictionary.ImEx.FileId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
			}

		return state;
		}

		private static readonly String fDelete = "DELETE * FROM CAP.courses WHERE coursesId =" + DataDictImEx.ParFileId; 
	}
}
