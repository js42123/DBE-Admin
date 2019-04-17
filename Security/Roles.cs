using System;
using System.Collections.Generic;
using System.Text;

namespace DBESearchAdmin.Security
{
	internal static class Roles
	{
		internal const string View = "View";
		internal const string Modify = "Modify";
		internal const string Developer = "Developer";

		internal const string AllRoles = "View, Modify, Developer";

		internal static string[] GetAllRoleDisplayNames()
		{
			List<string> roles = new List<string>();

			roles.Add("View");
			roles.Add("Modify");
			roles.Add("Developer");

			return roles.ToArray();
		}
		public static string GetRoleDisplayName(string role)
		{
			if (String.IsNullOrEmpty(role))
				return String.Empty;
			return role;
		}
	}
}
