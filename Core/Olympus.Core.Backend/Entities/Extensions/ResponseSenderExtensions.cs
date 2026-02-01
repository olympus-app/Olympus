namespace Olympus.Core.Backend.Entities;

public static class ResponseSenderExtensions {

	extension(IResponseSender sender) {

		public Task<Void> CreatedAsync<TReadResponse>(TReadResponse response, CancellationToken cancellationToken = default) where TReadResponse : class, IEntityReadResponse {

			if (response.ETag is not null) sender.HttpContext.Response.Headers.ETag = EntityTag.From(response.ETag);

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.Created.Value, null, cancellationToken);

		}

		public Task<Void> UpdatedAsync<TReadResponse>(TReadResponse response, CancellationToken cancellationToken = default) where TReadResponse : class, IEntityReadResponse {

			if (response.ETag is not null) sender.HttpContext.Response.Headers.ETag = EntityTag.From(response.ETag);

			return sender.HttpContext.Response.SendAsync(response, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> DeletedAsync<TEntity>(TEntity? entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity {

			if (entity?.ETag is not null) sender.HttpContext.Response.Headers.ETag = EntityTag.From(entity.ETag);

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.NoContent.Value, cancellationToken);

		}

		public Task<Void> ItemAsync<TReadResponse>(TReadResponse item, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TReadResponse : class, IEntityReadResponse {

			var etag = EntityTag.From(item.ETag);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendAsync(item, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public async Task<Void> ListAsync<TListResponse>(IQueryable<TListResponse> projection, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TListResponse : class, IEntityListResponse {

			var items = await projection.ToListAsync(cancellationToken);

			var result = new EntityListResult<TListResponse>(projection);

			var etag = EntityTag.From(result);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return await sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return await sender.HttpContext.Response.SendAsync(result, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public async Task<Void> PageAsync<TQueryRequest, TQueryResponse>(IQueryable<TQueryResponse> projection, TQueryRequest request, string cacheControl = ResponseCache.PrivateRevalidate, CancellationToken cancellationToken = default) where TQueryRequest : class, IEntityQueryRequest where TQueryResponse : class, IEntityQueryResponse {

			var config = sender.HttpContext.RequestServices.GetService<IEntityQueryConfiguration<TQueryResponse>>();

			var options = new GridifyQuery(request.Page ?? 1, request.PageSize ?? 10, request.Filter, request.OrderBy);

			if (!options.IsValid(config)) return await sender.BadRequestAsync(ErrorsStrings.Values.InvalidQuery, cancellationToken);

			var items = await projection.GridifyAsync(options, cancellationToken, config);

			var result = new EntityPageResult<TQueryResponse>(options.Page, options.PageSize, items.Data, items.Count);

			var etag = EntityTag.From(result);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return await sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return await sender.HttpContext.Response.SendAsync(result, HttpStatusCode.OK.Value, null, cancellationToken);

		}

		public Task<Void> FileAsync<TStorageEntity>(Stream stream, TStorageEntity entity, string cacheControl = ResponseCache.PrivateRevalidate, bool enableRangeProcessing = false, CancellationToken cancellationToken = default) where TStorageEntity : class, IStorageEntity {

			var etag = EntityTag.From(entity.ETag);

			if (EntityTag.IfNoneMatch(sender.HttpContext.Request.Headers.ETag, etag)) return sender.NotModifiedAsync(cancellationToken);

			if (!string.IsNullOrEmpty(etag)) sender.HttpContext.Response.Headers.ETag = etag;

			if (!string.IsNullOrEmpty(cacheControl)) sender.HttpContext.Response.Headers.CacheControl = cacheControl;

			return sender.HttpContext.Response.SendStreamAsync(stream, entity.Name, entity.Size, entity.ContentType, entity.UpdatedAt, enableRangeProcessing, cancellationToken);

		}

		public Task<Void> UploadedAsync<TStorageEntity>(TStorageEntity entity, CancellationToken cancellationToken = default) where TStorageEntity : class, IStorageEntity {

			if (entity.ETag is not null) sender.HttpContext.Response.Headers.ETag = EntityTag.From(entity.ETag);

			return sender.HttpContext.Response.SendStatusCodeAsync(HttpStatusCode.OK.Value, cancellationToken);

		}

	}

}
