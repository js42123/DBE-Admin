using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AHTD.Security.Common;
using AHTD.Security.Web;
using System.Configuration;

namespace $rootnamespace$.Security
{
	/// <summary>
	/// Provides info related to the current user's claims data.
	/// </summary>
	public class CurrentUser : UserInfoBase
	{

		// This code will add claims data support when running locally in Visual Studio

        protected override IEnumerable<ClientClaim> GetClientClaims()
        {

            if (ConfigurationManager.AppSettings["RuntimeEnvironment"].Equals("local"))
            {
                List<ClientClaim> claims = new List<ClientClaim>();

                claims.Add(new ClientClaim(StandardClaimTypes.WindowsAccountName, HttpContext.Current.User.Identity.Name));
                claims.Add(new ClientClaim(StandardClaimTypes.Role, Roles.Developer));

                return claims;
            }

            return base.GetClientClaims();
        }


		/// <summary>
		/// Gets the current user's security role.
		/// </summary>
		public string Role
		{
			get
			{
				return GetSpecificClaimValue(StandardClaimTypes.Role);
			}
		}
	}
}
