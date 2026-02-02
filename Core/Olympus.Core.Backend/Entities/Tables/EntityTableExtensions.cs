namespace Olympus.Core.Backend.Entities;

public static class EntityTableExtensions {

	extension<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity {

		public void Prepare(string tableName, string schemaName) {

			builder.ToTable(tableName, schemaName);

			builder.HasKey(entity => entity.Id);

			builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
			builder.Property(entity => entity.ETag).IsConcurrencyToken();

			builder.HasOne(entity => entity.CreatedBy).WithMany().HasForeignKey(entity => entity.CreatedById).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(entity => entity.UpdatedBy).WithMany().HasForeignKey(entity => entity.UpdatedById).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(entity => entity.DeletedBy).WithMany().HasForeignKey(entity => entity.DeletedById).OnDelete(DeleteBehavior.Restrict);

		}

	}

	extension<TEntity>(IndexBuilder<TEntity> builder) where TEntity : class, IEntity {

		public IndexBuilder<TEntity> IsUniqueWhenNotSoftDeleted() {

			return builder.IsUnique().HasAnnotation(IDatabaseService.SoftDeleteIndexAnnotation, true);

		}

	}

}
