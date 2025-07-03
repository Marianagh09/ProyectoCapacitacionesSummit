using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sif;
using Sif.Security;

namespace CAP.Auth
{
	public class LdapUserData
	{
		public string Username { get; set; } = "";
		public string FullName { get; set; } = "";
		public string Email { get; set; } = "";
		public string Organization { get; set; } = "";
		public string Role { get; set; } = "";
	}

	public class GetUserAttributeService
	{
		private readonly String ldapPath;
		private readonly String ldapUser;
		private readonly String ldapPassword;


		public GetUserAttributeService (String ldapPath, String ldapUser, String ldapPassword)
		{
			this.ldapPath = ldapPath;
			this.ldapUser = ldapUser;
			this.ldapPassword = ldapPassword;
		}

		public LdapUserData? GetUserBySamAccountName(String username)
		{
			try 
			{
				using (var entry = new DirectoryEntry(ldapPath, ldapUser, ldapPassword))
				{
					using (var searcher = new DirectorySearcher(entry))
					{
						searcher.Filter = $"(sAMAccountName={username})";
						searcher.PropertiesToLoad.Add("displayName");
						searcher.PropertiesToLoad.Add("mail");
						searcher.PropertiesToLoad.Add("company");
						searcher.PropertiesToLoad.Add("title");


						var result = searcher.FindOne();
						if (result != null)
						{
							var data = new LdapUserData
							{
								Username = username,
								FullName = GetProperty(result, "displayName"),
								Email = GetProperty(result, "mail"),
								Organization = GetProperty(result, "company"),
								Role = GetProperty(result, "title")
							};
							
							return data;
						}
					}
				}
			} catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message);
				return null;
			}
			return null;
		}

		private String GetProperty(SearchResult result, String name)
		{
			if (result.Properties.Contains(name) && result.Properties[name].Count > 0)
			{
				return result.Properties[name][0].ToString();
			}
			return String.Empty;
		}

		public static implicit operator ServiceState(GetUserAttributeService v)
		{
			throw new NotImplementedException();
		}
	}
}
