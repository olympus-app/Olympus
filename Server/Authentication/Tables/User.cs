using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympus.Server.Database;
using Olympus.Server.Statics;

namespace Olympus.Server.Authentication;

public class UserTable : IEntityTypeConfiguration<User> {

	public void Configure(EntityTypeBuilder<User> builder) {

		// Base Configuration
		builder.BaseConfiguration();

		// Relationships
		builder.HasMany(u => u.Identities).WithOne(i => i.User).HasForeignKey(i => i.UserID).OnDelete(DeleteBehavior.Cascade);

		// Indexes
		builder.HasIndex(u => u.Email).IsUnique();

		// Seed
		builder.HasData(
			new {
				ID = SystemVars.ExternalUserID,
				Name = "External User",
				Email = "external.user@olympus.com",
				CreatedBy = SystemVars.ExternalUserID,
				CreatedAt = SystemVars.DefaultCreatedAt,
				UpdatedBy = SystemVars.ExternalUserID,
				UpdatedAt = SystemVars.DefaultUpdatedAt,
				Active = false,
				Hidden = true
			},
			new {
				ID = SystemVars.ServiceUserID,
				Name = "Service User",
				Email = "service.user@olympus.com",
				CreatedBy = SystemVars.ServiceUserID,
				CreatedAt = SystemVars.DefaultCreatedAt,
				UpdatedBy = SystemVars.ServiceUserID,
				UpdatedAt = SystemVars.DefaultUpdatedAt,
				Active = false,
				Hidden = true
			},
			new {
				ID = SystemVars.SystemUserID,
				Name = "System User",
				Email = "system.user@olympus.com",
				CreatedBy = SystemVars.SystemUserID,
				CreatedAt = SystemVars.DefaultCreatedAt,
				UpdatedBy = SystemVars.SystemUserID,
				UpdatedAt = SystemVars.DefaultUpdatedAt,
				Active = false,
				Hidden = true
			}
		);

	}

}
