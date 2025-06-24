using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Users
{
	public class PutUserInfoBusiness : BusinessService
	{
		public PutUserInfoBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PutUserInfoData(this.Dictionary));
		}
	}
}
