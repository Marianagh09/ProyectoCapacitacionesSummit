using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				var response = this.StartService(new GetUserAttributesData(this.Dictionary));
				return response;

			}
			return state;
		}
	}
}
