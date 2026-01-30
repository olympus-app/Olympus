using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.Caching.Hybrid;

namespace Olympus.Api.Database;

public class CacheProvider(HybridCache hybridCache) : IEFCacheServiceProvider {

	private readonly HybridCache HybridCache = hybridCache;

	private readonly TimeSpan LocalExpiration = 30.Minutes();

	private readonly MessagePackSerializerOptions SerializerOptions = GetSerializerOptions();

	private static MessagePackSerializerOptions GetSerializerOptions() {

		var resolver = CompositeResolver.Create(NativeDateTimeResolver.Instance, TypelessObjectResolver.Instance, ContractlessStandardResolver.Instance);

		return MessagePackSerializerOptions.Standard.WithResolver(resolver).WithCompression(MessagePackCompression.Lz4BlockArray);

	}

	public EFCachedData? GetValue(EFCacheKey cacheKey, EFCachePolicy cachePolicy) {

		try {

			static async ValueTask<byte[]?> factory(CancellationToken cancel) => await Task.FromResult<byte[]?>(null);

			var options = new HybridCacheEntryOptions() { Expiration = cachePolicy.CacheTimeout, LocalCacheExpiration = LocalExpiration };

			var bytes = HybridCache.GetOrCreateAsync(cacheKey.KeyHash, factory, options).AsTask().GetAwaiter().GetResult();

			if (bytes is null || bytes.Length == 0) return null;

			return MessagePackSerializer.Deserialize<EFCachedData>(bytes, SerializerOptions);

		} catch {

			InvalidateCacheDependencies(cacheKey);

			return null;

		}

	}

	public void InsertValue(EFCacheKey cacheKey, EFCachedData? value, EFCachePolicy cachePolicy) {

		if (value is null) return;

		try {

			var bytes = MessagePackSerializer.Serialize(value, SerializerOptions);

			var options = new HybridCacheEntryOptions() { Expiration = cachePolicy.CacheTimeout, LocalCacheExpiration = LocalExpiration };

			HybridCache.SetAsync(cacheKey.KeyHash, bytes, options).AsTask().GetAwaiter().GetResult();

		} catch (Exception exception) {

			Console.WriteLine(exception.Message);

		}

	}

	public void InvalidateCacheDependencies(EFCacheKey cacheKey) {

		try {

			HybridCache.RemoveAsync(cacheKey.KeyHash).AsTask().GetAwaiter().GetResult();

		} catch { }

	}

	public void ClearAllCachedEntries() { }

}
