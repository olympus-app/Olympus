using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Olympus.Api.Database;

public static class ModelBuilderExtensions {

	extension(ModelBuilder builder) {

		public void ApplyConfigurations(DatabaseFacade database, IEnumerable<IEntityTable> tables, AppSettings settings) {

			using (new AppLocalizationScope(settings)) {

				var softDeleteSql = GetSoftDeleteFilterSql(database);

				foreach (var table in tables) table.Apply(builder);

				foreach (var entityType in builder.Model.GetEntityTypes()) {

					foreach (var index in entityType.GetIndexes()) {

						if (index.FindAnnotation(IDatabaseContext.SoftDeleteIndexAnnotation) is not null) {

							index.RemoveAnnotation(IDatabaseContext.SoftDeleteIndexAnnotation);
							index.SetFilter(softDeleteSql);

						}

					}

				}

			}

		}

	}

	private static string? GetSoftDeleteFilterSql(DatabaseFacade database) {

		if (database.IsPostgreSql()) return $"\"{nameof(IEntity.IsDeleted)}\" = false";

		if (database.IsSqlServer()) return $"[{nameof(IEntity.IsDeleted)}] = 0";

		return $"{nameof(IEntity.IsDeleted)} = 0";

	}

}
