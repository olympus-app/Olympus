using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympus.Server.Database;

namespace Olympus.Server.Authentication;

public class UserIdentityTable : IEntityTypeConfiguration<UserIdentity> {

	public void Configure(EntityTypeBuilder<UserIdentity> builder) {

		// Base Configuration
		builder.BaseConfiguration();

		// Indexes
		builder.HasIndex(i => new { i.ProviderName, i.ProviderKey }).IsUnique();
		builder.HasIndex(i => new { i.ProviderUpn }).IsUnique();

	}

}
