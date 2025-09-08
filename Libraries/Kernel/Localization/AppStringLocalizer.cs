using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Localization;

namespace Olympus.Kernel;

public class AppStringLocalizer(ResourceManager hostResourceManager, ResourceManager originalResourceManager) : IStringLocalizer {

	public LocalizedString this[string name] {

		get {

			try {

				var value = hostResourceManager.GetString(name, CultureInfo.CurrentUICulture);
				if (value != null) return new LocalizedString(name, value, resourceNotFound: false);

			} catch { }

			try {

				var value = originalResourceManager.GetString(name, CultureInfo.CurrentUICulture);
				if (value != null) return new LocalizedString(name, value, resourceNotFound: false);

			} catch { }

			return new LocalizedString(name, name, resourceNotFound: true);

		}

	}

	public LocalizedString this[string name, params object[] arguments] => this[name];

	public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) {

		var hostStrings = hostResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<System.Collections.DictionaryEntry>()
			.ToDictionary(e => e.Key.ToString()!, e => e.Value!.ToString()!) ?? [];

		var originalStrings = originalResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<System.Collections.DictionaryEntry>()
			.ToDictionary(e => e.Key.ToString()!, e => e.Value!.ToString()!) ?? [];

		foreach (var item in originalStrings) {

			if (!hostStrings.ContainsKey(item.Key)) {

				hostStrings[item.Key] = item.Value;

			}

		}

		return hostStrings.Select(e => new LocalizedString(e.Key, e.Value, false));

	}

}
