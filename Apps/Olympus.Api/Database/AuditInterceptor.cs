using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Olympus.Api.Database;

public class AuditInterceptor : SaveChangesInterceptor {

	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {

		PrepareEntities(eventData.Context);
		return base.SavingChanges(eventData, result);

	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) {

		PrepareEntities(eventData.Context);
		return base.SavingChangesAsync(eventData, result, cancellationToken);

	}

	private static void PrepareEntities(DbContext? context) {

		if (context is null) return;

		var entries = context.ChangeTracker.Entries<IEntity>();

		foreach (var entry in entries) {

			if (entry.State == EntityState.Added) {

				entry.Property(e => e.CreatedById).CurrentValue = AppUsers.Unknown.Id;
				entry.Property(e => e.UpdatedById).CurrentValue = AppUsers.Unknown.Id;
				entry.Property(e => e.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
				entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
				entry.Property(e => e.Version).CurrentValue = Guid.NewGuidv7();

			}

			if (entry.State == EntityState.Modified) {

				entry.Property(e => e.CreatedById).CurrentValue ??= AppUsers.Unknown.Id;
				entry.Property(e => e.UpdatedById).CurrentValue = AppUsers.Unknown.Id;
				entry.Property(e => e.CreatedAt).CurrentValue ??= DateTimeOffset.UtcNow;
				entry.Property(e => e.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
				entry.Property(e => e.Version).CurrentValue = Guid.NewGuidv7();
			}

		}

	}

}
