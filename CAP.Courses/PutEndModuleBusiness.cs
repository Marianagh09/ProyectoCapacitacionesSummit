using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class PutEndModuleBusiness : BusinessService
	{
		public PutEndModuleBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PutEndModuleData(this.Dictionary));
		}
	}
}
