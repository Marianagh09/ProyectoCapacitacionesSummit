using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class GetCompletedCoursesBusiness : BusinessService
	{
		public GetCompletedCoursesBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new GetCopmletedCoursesData(this.Dictionary));
		}
	}
}
