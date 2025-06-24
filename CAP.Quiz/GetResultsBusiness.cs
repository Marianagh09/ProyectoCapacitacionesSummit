using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Quiz
{
	public class GetResultsBusiness : BusinessService
	{
		public GetResultsBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new GetResultsData(this.Dictionary));
		}
	}
}
