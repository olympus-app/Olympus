namespace Olympus.Core.Backend.Users;

public class UsersTable : EntityTable<User> {

	public override void Configure(EntityTypeBuilder<User> builder) {

		base.Configure(builder);

		builder.Property(e => e.Name).HasMaxLength(64);
		builder.Property(e => e.Email).HasMaxLength(64);
		builder.Property(e => e.Username).HasMaxLength(64);
		builder.Property(e => e.Password).HasMaxLength(64);
		builder.Property(e => e.JobTitle).HasMaxLength(64);
		builder.Property(e => e.Department).HasMaxLength(64);
		builder.Property(e => e.OfficeLocation).HasMaxLength(64);
		builder.Property(e => e.Country).HasMaxLength(64);

		// builder.HasMany(u => u.Identities).WithOne(i => i.User).HasForeignKey(i => i.Identifier).OnDelete(DeleteBehavior.Cascade);

		builder.HasIndex(u => u.Email).IsUnique();
		builder.HasIndex(u => u.Username).IsUnique();

		builder.HasData(
			new {
				Id = AppUsers.Unknown.Id,
				Name = AppUsers.Unknown.Name,
				Email = AppUsers.Unknown.Email,
				CreatedById = AppUsers.Unknown.Id,
				CreatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				UpdatedById = AppUsers.Unknown.Id,
				UpdatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				Active = false,
				Hidden = true,
				Locked = true,
				Version = Guid.Empty
			},
			new {
				Id = AppUsers.External.Id,
				Name = AppUsers.External.Name,
				Email = AppUsers.External.Email,
				CreatedById = AppUsers.External.Id,
				CreatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				UpdatedById = AppUsers.External.Id,
				UpdatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				Active = false,
				Hidden = true,
				Locked = true,
				Version = Guid.Empty
			},
			new {
				Id = AppUsers.Service.Id,
				Name = AppUsers.Service.Name,
				Email = AppUsers.Service.Email,
				CreatedById = AppUsers.Service.Id,
				CreatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				UpdatedById = AppUsers.Service.Id,
				UpdatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				Active = false,
				Hidden = true,
				Locked = true,
				Version = Guid.Empty
			},
			new {
				Id = AppUsers.Agent.Id,
				Name = AppUsers.Agent.Name,
				Email = AppUsers.Agent.Email,
				CreatedById = AppUsers.Agent.Id,
				CreatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				UpdatedById = AppUsers.Agent.Id,
				UpdatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				Active = false,
				Hidden = true,
				Locked = true,
				Version = Guid.Empty
			},
			new {
				Id = AppUsers.System.Id,
				Name = AppUsers.System.Name,
				Email = AppUsers.System.Email,
				CreatedById = AppUsers.System.Id,
				CreatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				UpdatedById = AppUsers.System.Id,
				UpdatedAt = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero),
				Active = false,
				Hidden = true,
				Locked = true,
				Version = Guid.Empty
			}
		);

	}

}
