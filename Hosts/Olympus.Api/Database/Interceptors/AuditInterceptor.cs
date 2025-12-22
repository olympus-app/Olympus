using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Olympus.Api.Database;

public class AuditInterceptor(IHttpContextAccessor accessor) : SaveChangesInterceptor {

	private ClaimsPrincipal? User => accessor.HttpContext?.User;

	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {

		SetAuditFields(eventData.Context);

		return base.SavingChanges(eventData, result);

	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) {

		SetAuditFields(eventData.Context);

		return base.SavingChangesAsync(eventData, result, cancellationToken);

	}

	private void SetAuditFields(DbContext? context) {

		if (context is null) return;

		foreach (var entry in context.ChangeTracker.Entries<IEntity>()) {

			if (entry.State == EntityState.Added) {

				entry.Property(entity => entity.ETag).CurrentValue = Guid.NewGuidV7();
				entry.Property(entity => entity.CreatedById).CurrentValue = User?.Id ?? AppUsers.Unknown.Id;
				entry.Property(entity => entity.UpdatedById).CurrentValue = User?.Id ?? AppUsers.Unknown.Id;
				entry.Property(entity => entity.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
				entry.Property(entity => entity.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;

			} else if (entry.State == EntityState.Modified) {

				if (entry.Property(entity => entity.IsDeleted).CurrentValue && !entry.Property(entity => entity.IsDeleted).OriginalValue) {

					entry.Property(entity => entity.ETag).CurrentValue = Guid.NewGuidV7();
					entry.Property(entity => entity.UpdatedById).CurrentValue = User?.Id ?? AppUsers.Unknown.Id;
					entry.Property(entity => entity.DeletedById).CurrentValue = User?.Id ?? AppUsers.Unknown.Id;
					entry.Property(entity => entity.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;
					entry.Property(entity => entity.DeletedAt).CurrentValue = DateTimeOffset.UtcNow;

				} else {

					entry.Property(entity => entity.ETag).CurrentValue = Guid.NewGuidV7();
					entry.Property(entity => entity.CreatedById).CurrentValue ??= User?.Id ?? AppUsers.Unknown.Id;
					entry.Property(entity => entity.UpdatedById).CurrentValue = User?.Id ?? AppUsers.Unknown.Id;
					entry.Property(entity => entity.CreatedAt).CurrentValue ??= DateTimeOffset.UtcNow;
					entry.Property(entity => entity.UpdatedAt).CurrentValue = DateTimeOffset.UtcNow;

				}

			}

		}

	}

}
