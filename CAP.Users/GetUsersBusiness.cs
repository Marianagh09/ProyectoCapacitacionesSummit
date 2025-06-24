using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Users
{
	public class GetUsersBusiness : BusinessService
	{
		public GetUsersBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new GetUsersData(this.Dictionary));
		}
	}
}
