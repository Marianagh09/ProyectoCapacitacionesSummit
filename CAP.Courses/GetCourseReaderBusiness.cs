using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class GetCourseReaderBusiness : BusinessService
	{
		public GetCourseReaderBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new GetCourseReaderData(this.Dictionary));
		}
	}
}
