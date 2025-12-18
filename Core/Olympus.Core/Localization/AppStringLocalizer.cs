using System.Collections;
using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Localization;

namespace Olympus.Core.Localization;

public class AppStringLocalizer(ResourceManager sourceResourceManager, ResourceManager hostResourceManager) : IStringLocalizer {

	public LocalizedString this[string name] {
		get {

			var value = hostResourceManager.GetString(name, CultureInfo.CurrentUICulture);
			if (value is not null) return new LocalizedString(name, value, resourceNotFound: false);

			value = sourceResourceManager.GetString(name, CultureInfo.CurrentUICulture);
			if (value is not null) return new LocalizedString(name, value, resourceNotFound: false);

			return new LocalizedString(name, name, resourceNotFound: true);

		}
	}

	public LocalizedString this[string name, params object[] arguments] => this[name];

	public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) {

		var hostStrings = hostResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<DictionaryEntry>().ToDictionary(entry => entry.Key.ToString()!, entry => entry.Value!.ToString()!) ?? [];

		var originalStrings = sourceResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, includeParentCultures)?
			.Cast<DictionaryEntry>().ToDictionary(entry => entry.Key.ToString()!, entry => entry.Value!.ToString()!) ?? [];

		foreach (var item in originalStrings) {

			if (!hostStrings.ContainsKey(item.Key)) {

				hostStrings[item.Key] = item.Value;

			}

		}

		return hostStrings.Select(entry => new LocalizedString(entry.Key, entry.Value, false));

	}

}
