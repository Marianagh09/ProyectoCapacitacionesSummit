using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Auth
{
	public class GetUserBusiness : BusinessService
	{
		public GetUserBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new GetUserData(this.Dictionary));
		}
	}
}
