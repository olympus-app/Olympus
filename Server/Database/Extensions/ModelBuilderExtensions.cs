using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olympus.Server.System;

namespace Olympus.Server.Database;

public static class ModelBuilderExtensions {

	public static void BaseConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity {

		// Primary Key
		builder.HasKey(e => e.ID);

		// Column Configurations
		builder.Property(e => e.ID).ValueGeneratedOnAdd();
		builder.Property(e => e.Active).HasDefaultValue(true);
		builder.Property(e => e.Hidden).HasDefaultValue(false);

		// Navigation Properties
		builder.HasOne(e => e.CreatedUser).WithMany().HasForeignKey(e => e.CreatedBy).OnDelete(DeleteBehavior.Restrict);
		builder.HasOne(e => e.UpdatedUser).WithMany().HasForeignKey(e => e.UpdatedBy).OnDelete(DeleteBehavior.Restrict);

	}

}
