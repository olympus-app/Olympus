using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Olympus.Server.Configuration;

public static class ODataConfig {

	public static IEdmModel GetEdmModel() {

		ODataConventionModelBuilder builder = new();
		return builder.GetEdmModel();

	}

}
