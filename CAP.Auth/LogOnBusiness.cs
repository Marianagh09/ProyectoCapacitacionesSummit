using System.Runtime.Serialization.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
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
							//"jusmeg@summit.com.co",
							//"Code.*+1012"
							 this.Dictionary.Ldap.LdapLogOn,
							 this.Dictionary.Ldap.Password
				);

				var userData = GetUserAttributeService.GetUserBySamAccountName(this.Dictionary.Security.UserLogOn);

				if (userData != null)
				{
					//set dictionary field to be stored in DB
					this.Dictionary.Security.NewFirstName = userData.FullName;
					this.Dictionary.Security.UserName = userData.Email;
					_ = this.StartService(new PostNewUserBusiness(this.Dictionary));

					var configBuilder = new ConfigurationBuilder()
					.SetBasePath(AppContext.BaseDirectory) // Establece el directorio base de ejecución
					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

					IConfiguration manualConfig = configBuilder.Build();

					var jwtService = new StartJWT(manualConfig);

					//this.Dictionary.Sif.JsonResponseObject = $"{userData.Username}, {userData.Email}, {userData.Organization}, {userData.Role}";
					String token = jwtService.GenerateToken(userData.Email, userData.FullName, userData.Role);

					this.Dictionary.Sif.LoggedUser = userData.Username;
					this.Dictionary.Customers.CustomerNames = userData.FullName;
					this.Dictionary.Customers.Address = userData.Email;
					this.Dictionary.Roles.RoleName = userData.Role;
					this.Dictionary.Security.SessionTicket = token;
					//this.Dictionary.Security.TellerId = userData.UserId;

					var responseObject = new
					{
						Username = this.Dictionary.Sif.LoggedUser,
						FullName = this.Dictionary.Customers.CustomerNames,
						Email = this.Dictionary.Customers.Address,
						Role = this.Dictionary.Roles.RoleName,
						Token = this.Dictionary.Security.SessionTicket, 
						UserId = this.Dictionary.Security.TellerId
					};

					this.Dictionary.Sif.JsonResponseObject = JsonSerializer.Serialize(responseObject);

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
