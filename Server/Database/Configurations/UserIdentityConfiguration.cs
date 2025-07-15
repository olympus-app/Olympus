using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympus.Server.Authentication;

namespace Olympus.Server.Database;

public class UserIdentityonfiguration : IEntityTypeConfiguration<UserIdentity> {

	public void Configure(EntityTypeBuilder<UserIdentity> builder) {

		// Base Configuration
		builder.BaseConfiguration();

		// Indexes
		builder.HasIndex(i => new { i.ProviderName, i.ProviderKey }).IsUnique();
		builder.HasIndex(i => new { i.ProviderUpn }).IsUnique();

	}

}
