using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Quiz
{
	public class PutUpdateQuizBusiness : BusinessService
	{
		public PutUpdateQuizBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PutUpdateQuizData(this.Dictionary));
		}
	}
}
