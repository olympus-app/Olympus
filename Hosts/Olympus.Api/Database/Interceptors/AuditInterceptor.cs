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

	public override int SavedChanges(SaveChangesCompletedEventData eventData, int result) {

		HydrateAuditFields(eventData.Context);

		return base.SavedChanges(eventData, result);

	}

	public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default) {

		HydrateAuditFields(eventData.Context);

		return base.SavedChangesAsync(eventData, result, cancellationToken);

	}

	private void SetAuditFields(DbContext? context) {

		if (context is null) return;

		foreach (var entry in context.ChangeTracker.Entries<IEntity>().Where(entity => entity.State != EntityState.Detached)) {

			if (entry.State == EntityState.Added) {

				entry.Property(entity => entity.Id).CurrentValue = Guid.NewGuidV7();
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

	private void HydrateAuditFields(DbContext? context) {

		var user = User?.AsEntity();

		if (context is null || user is null) return;

		foreach (var entry in context.ChangeTracker.Entries<IEntity>().Where(entity => entity.State != EntityState.Detached)) {

			if (entry.Entity.CreatedById == user.Id) entry.Entity.CreatedBy = user;
			if (entry.Entity.UpdatedById == user.Id) entry.Entity.UpdatedBy = user;
			if (entry.Entity.DeletedById == user.Id) entry.Entity.DeletedBy = user;

		}

	}

}
