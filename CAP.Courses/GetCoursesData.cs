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
	public class GetCoursesData : DataService
	{
		public GetCoursesData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurses, this.Connection))
			{
 				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "", "", true);
				state = ServiceState.Accepted; 
			}

			return state;
		}

		private static readonly String fCurses = "Select * from CAP.Courses "; 
	}
}
