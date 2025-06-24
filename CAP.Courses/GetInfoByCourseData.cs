using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Services;

namespace CAP.Courses
{
	public class GetInfoByCourseData : DataService
	{
		public GetInfoByCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurses, this.Connection))
			{
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.FieldIdName, this.Dictionary.ImEx.FieldId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);
				state = ServiceState.Accepted;
			}

			return state;
		}

		private static readonly String fCurses = "Select * from CAP.Courses where coursesId = " + DataDictImEx.ParFieldId;
	}
}
