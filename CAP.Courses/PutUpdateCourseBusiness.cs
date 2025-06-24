using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class PutUpdateCourseBusiness : BusinessService
	{
		public PutUpdateCourseBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PutUpdateCourseData(this.Dictionary));
		}
	}
}
