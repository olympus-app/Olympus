using System.ComponentModel.DataAnnotations;
using Olympus.Server.System;

namespace Olympus.Server.Authentication;

public class UserIdentity : BaseEntity {

	public Guid UserID { get; protected set; }
	public virtual User User { get; protected set; }

	[MaxLength(64)]
	public string ProviderName { get; protected set; }

	[MaxLength(64)]
	public string ProviderUpn { get; protected set; }

	[MaxLength(64)]
	public string ProviderKey { get; protected set; }

	private UserIdentity() {

		User = null!;
		ProviderName = null!;
		ProviderUpn = null!;
		ProviderKey = null!;

	}

	public UserIdentity(User user, string providerName, string providerUpn, string providerKey) : base() {

		UserID = user.ID;
		User = user;

		ProviderName = providerName;
		ProviderUpn = providerUpn;
		ProviderKey = providerKey;

	}

	public void UpdateUpn(string upn) {

		ProviderUpn = upn;

	}

}
