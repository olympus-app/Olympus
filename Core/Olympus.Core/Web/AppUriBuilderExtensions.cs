namespace Olympus.Core.Web;

public static class AppUriBuilderExtensions {

	extension(AppUriBuilder builder) {

		public AppUriBuilder WithId(object? value, bool append = true) {

			return builder.WithQuery("id", value, append);

		}

		public AppUriBuilder WithCacheBusting(object? value, bool append = true) {

			if (value is null) return builder;

			return builder.WithQuery("v", value.ToString(), append);

		}

		public AppUriBuilder WithCacheBusting(DateTime? value, bool append = true) {

			if (value is null) return builder;

			return builder.WithQuery("v", value.Value.Ticks.ToString("X"), append);

		}

		public AppUriBuilder WithCacheBusting(DateTimeOffset? value, bool append = true) {

			if (value is null) return builder;

			return builder.WithQuery("v", value.Value.Ticks.ToString("X"), append);

		}

	}

}
