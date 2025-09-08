namespace Olympus.Aether.Backend;

public class ExamplesModel : EntityModel<Example> {

	protected override void Configure(ODataModelBuilder builder) {

		base.Configure(builder);

		builder.EntitySet<Example>();

	}

}
