using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Microsoft.IdentityModel.Claims;

using AHTD.Security.Common;

namespace AHTD.Security.Web
{
	public interface IUserInfoBase
	{
		string WindowsAccountName { get; }
	}

	/// <summary>
	/// A base class for info related to a user's claims data.
	/// </summary>
	/// <remarks>
	/// Inherit from this class in your web app and extend with values that are
	/// relavent, such as perhaps employee number or role.
	/// </remarks>
	public abstract class UserInfoBase : IUserInfoBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserInfoBase"/> class.
		/// </summary>
		public UserInfoBase()
		{
			ClientClaims = GetClientClaims();
		}

		/// <summary>
		/// Gets the current user's Windows account name.
		/// </summary>
		public string WindowsAccountName
		{
			get { return GetSpecificClaimValue(StandardClaimTypes.WindowsAccountName); }
		}

		public IEnumerable<ClientClaim> ClientClaims { get; private set; }

		protected virtual IEnumerable<ClientClaim> GetClientClaims()
		{
			return GetClaimsFromSTSToken();
		}
		/// <summary>
		/// Gets a value for the specific claim.
		/// </summary>
		/// <param name="claimType">Type of the claim.</param>
		/// <returns></returns>
		protected string GetSpecificClaimValue(string claimType)
		{
			var specificClaim = ClientClaims.Where(cc => cc.ClaimType == claimType).FirstOrDefault();

			if (specificClaim == null)
				return null;
			else
				return specificClaim.Value;
		}
		protected IEnumerable<string> GetSpecificClaimValues(string claimType)
		{
			return ClientClaims.Where(c => c.ClaimType == claimType).Select(c => c.Value);
		}

		private IEnumerable<ClientClaim> GetClaimsFromSTSToken()
		{
			var claimsIdentity = System.Threading.Thread.CurrentPrincipal.Identity as IClaimsIdentity;

			return claimsIdentity.Claims.Select(c => new ClientClaim(c.ClaimType, c.Value, c.ValueType));
		}
	}
}
