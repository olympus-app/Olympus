using Microsoft.EntityFrameworkCore;
using Olympus.Server.Authentication;
using Olympus.Server.System;

namespace Olympus.Server.Database;

public class DefaultDatabaseContext(DbContextOptions<DefaultDatabaseContext> options, ICurrentUserService currentUserService) : DbContext(options) {

	private readonly ICurrentUserService _currentUserService = currentUserService;

	#region Database Sets

	public DbSet<User> Users { get; set; }
	public DbSet<UserIdentity> UserIdentities { get; set; }

	#endregion

	#region Method Overrides

	protected override void OnModelCreating(ModelBuilder modelBuilder) {

		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDatabaseContext).Assembly);

	}

	#endregion

	#region Audit Fields Handling

	private void ApplyAuditInfo() {

		var auditables = ChangeTracker.Entries().Where(entry => entry.Entity is IAuditableEntity && (entry.State == EntityState.Added || entry.State == EntityState.Modified));

		foreach (var entry in auditables) {

			if (entry.State == EntityState.Added && entry.Entity is IAuditableEntity created) {

				created.SetCreated(_currentUserService.UserId);

			} else if (entry.State == EntityState.Modified && entry.Entity is IAuditableEntity updated) {

				updated.SetUpdated(_currentUserService.UserId);

			}

		}

	}

	public override int SaveChanges(bool acceptAllChangesOnSuccess) {

		ApplyAuditInfo();
		return base.SaveChanges(acceptAllChangesOnSuccess);

	}

	public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {

		ApplyAuditInfo();
		return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

	}

	#endregion

}
