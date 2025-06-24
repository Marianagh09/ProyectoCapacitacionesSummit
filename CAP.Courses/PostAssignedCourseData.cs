using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Data;
using Sif.Services;

namespace CAP.Courses
{
	public class PostAssignedCourseData : DataService
	{
		public PostAssignedCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fCurses, this.Connection))
			{
				  
			}
		}
	}
}
