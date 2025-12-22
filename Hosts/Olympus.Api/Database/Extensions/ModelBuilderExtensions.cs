using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Olympus.Api.Database;

public static class ModelBuilderExtensions {

	extension(ModelBuilder builder) {

		public void ApplyEntityConfigurations(DatabaseFacade database, IEnumerable<IEntityTable> tables, AppSettings settings) {

			using (new AppLocalizationScope(settings)) {

				var softDeleteSql = GetSoftDeleteFilterSql(database);

				foreach (var table in tables) table.Apply(builder);

				foreach (var entityType in builder.Model.GetEntityTypes()) {

					foreach (var index in entityType.GetIndexes()) {

						if (index.FindAnnotation(IEntityDatabase.SoftDeleteIndexAnnotation) is not null) {

							index.RemoveAnnotation(IEntityDatabase.SoftDeleteIndexAnnotation);
							index.SetFilter(softDeleteSql);

						}

					}

				}

			}

		}

		public void ApplyColumnsConventions() {

			foreach (var entityType in builder.Model.GetEntityTypes()) {

				foreach (var property in entityType.GetProperties()) {

					if (property.Name == nameof(IEntity.ETag)) property.IsConcurrencyToken = true;

				}

			}

		}

		public void ApplyColumnsOrder() {

			var systemColumns = new Dictionary<string, int> {
				{ nameof(IEntity.ETag), 9988 },
				{ nameof(IEntity.CreatedById), 9989 },
				{ nameof(IEntity.CreatedAt), 9990 },
				{ nameof(IEntity.UpdatedById), 9991 },
				{ nameof(IEntity.UpdatedAt), 9992 },
				{ nameof(IEntity.DeletedById), 9993 },
				{ nameof(IEntity.DeletedAt), 9994 },
				{ nameof(IEntity.IsActive), 9995 },
				{ nameof(IEntity.IsDeleted), 9996 },
				{ nameof(IEntity.IsHidden), 9997 },
				{ nameof(IEntity.IsLocked), 9998 },
				{ nameof(IEntity.IsSystem), 9999 },
			};

			foreach (var entityType in builder.Model.GetEntityTypes()) {

				var idIndex = 0;
				var baseIndex = 1000;
				var concreteIndex = 3000;
				var foreignIndex = 5000;
				var shadowIndex = 9000;
				var systemIndex = 9999;

				var allProps = entityType.GetProperties().ToList();

				var sortedProps = allProps.OrderBy(prop => {

					if (prop.Name == nameof(IEntity.Id)) return idIndex;

					if (systemColumns.ContainsKey(prop.Name)) return systemIndex;

					if (prop.IsShadowProperty()) return shadowIndex;

					var declaringType = prop.PropertyInfo?.DeclaringType;
					if (declaringType is null) return idIndex + 500;

					var isConcrete = declaringType == entityType.ClrType;

					if (declaringType.FullName?.StartsWith(AppSettings.AppBaseName) ?? false) {

						if (declaringType != entityType.ClrType) return idIndex + 100;

						return idIndex + 200;

					}

					return idIndex + 300;

				})
				.ThenBy(prop => prop.PropertyInfo?.MetadataToken ?? 0)
				.ThenBy(prop => prop.Name)
				.ToList();

				foreach (var property in sortedProps) {

					var propName = property.Name;

					if (propName == nameof(IEntity.Id)) {

						property.SetColumnOrder(0);

					} else if (systemColumns.TryGetValue(propName, out var order)) {

						property.SetColumnOrder(order);

					} else if (property.IsShadowProperty()) {

						property.SetColumnOrder(shadowIndex++);

					} else {

						if (property.PropertyInfo?.DeclaringType?.FullName?.StartsWith(AppSettings.AppBaseName) ?? false) {

							if (property.GetColumnOrder() is not null) continue;

							if (property.PropertyInfo?.DeclaringType != entityType.ClrType) {

								property.SetColumnOrder(baseIndex++);

							} else {

								property.SetColumnOrder(concreteIndex++);

							}

						} else {

							property.SetColumnOrder(foreignIndex++);

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
