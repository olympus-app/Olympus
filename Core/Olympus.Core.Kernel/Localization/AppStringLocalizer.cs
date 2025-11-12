using System.Collections;
using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Localization;

namespace Olympus.Core.Kernel.Localization;

public class AppStringLocalizer(ResourceManager sourceResourceManager, ResourceManager hostResourceManager) : IStringLocalizer {

	public LocalizedString this[string name] {

		get {

			try {

				var value = hostResourceManager.GetString(name, CultureInfo.CurrentUICulture);
				if (value is not null) return new LocalizedString(name, value, resourceNotFound: false);

			} catch { }

			try {

				var value = sourceResourceManager.GetString(name, CultureInfo.CurrentUICulture);
				if (value is not null) return new LocalizedString(name, value, resourceNotFound: false);

			} catch { }

			return new LocalizedString(name, name, resourceNotFound: true);

		}

	}

	public LocalizedString this[string name, params object[] arguments] => this[name];

	public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) {

		var hostStrings = hostResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<DictionaryEntry>().ToDictionary(e => e.Key.ToString()!, e => e.Value!.ToString()!) ?? [];

		var originalStrings = sourceResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<DictionaryEntry>().ToDictionary(e => e.Key.ToString()!, e => e.Value!.ToString()!) ?? [];

		foreach (var item in originalStrings) {

			if (!hostStrings.ContainsKey(item.Key)) {

				hostStrings[item.Key] = item.Value;

			}

		}

		return hostStrings.Select(e => new LocalizedString(e.Key, e.Value, false));

	}

}
