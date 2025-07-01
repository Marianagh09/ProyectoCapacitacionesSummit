using System.Runtime.Serialization.Json;
using System.Text.Json;
using Sif;
using Sif.Security.Ldap;
using Sif.Services;

namespace CAP.Auth
{
	public class LogOnBusiness : BusinessService
	{
		public LogOnBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			this.Dictionary.Ldap.LdapLogOn = this.Dictionary.Security.UserLogOn;
			this.Dictionary.Ldap.Password = this.Dictionary.Security.RawPassword;

			ServiceState state = this.StartService(new UserLogOnByAccountNameData(this.Dictionary));

			if (state == ServiceState.Accepted)
			{
				// Una vez se haya logueado el usuario, traer la data del usuario para hacer el registro en la db y poder hacer las relaciones
				var GetUserAttributeService = new GetUserAttributeService(
                            "LDAP://srvmedlan.summit.com.co",
							"jusmeg@summit.com.co",
							"Code.*+1012"
				);

				var userData = GetUserAttributeService.GetUserBySamAccountName(this.Dictionary.Security.UserLogOn);

				if ( userData != null)	
				{
					//this.Dictionary.Sif.JsonResponseObject = $"{userData.Username}, {userData.Email}, {userData.Organization}, {userData.Role}";
					this.Dictionary.Sif.JsonResponseObject = JsonSerializer.Serialize(userData);
					

					return ServiceState.Accepted;
				}
				else
				{
					return ServiceState.Rejected;
				}
			}

			return state;
		}
	}
}
