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
	public class GetCourseReaderData : DataService
	{
		public GetCourseReaderData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fReader, this.Connection))
			{
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.FileIdName, this.Dictionary.ImEx.FileId);
				this.Dictionary.Sif.JsonResponseObject = command.GetJsonResult(this.Message, "Courses", "Course", true);
				state = ServiceState.Accepted;
			}
			return state;
		}

		private static readonly String fReader = "SELECT" +
			"  c.title AS title, " +
			"  m.name AS name, " +
			"  m.descripcion AS descripcion, " +
			"  f.fileurl AS urlf " +
			"FROM  CAP.Courses c " +
			"JOIN CAP.Modules m ON m.course_id = c.coursesid " +
			"LEFT JOIN CAP.Files f ON f.module_id = m.moduleid " +
			"WHERE c.coursesid = " + DataDictImEx.ParFileId; 
	}
}
