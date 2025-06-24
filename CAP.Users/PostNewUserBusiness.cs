using Sif;
using Sif.Services;

namespace CAP.Users
{
	public class PostNewUserBusiness : BusinessService
	{
		public PostNewUserBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PostNewUserData(this.Dictionary));
		}
	}
}
