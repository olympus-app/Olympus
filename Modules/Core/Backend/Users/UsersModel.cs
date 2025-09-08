namespace Olympus.Core.Backend;

public class UsersModel(IOptions<CoreOptions> options) : EntityModel<User, UserModel, UsersController> {

	protected override string ModulePrefix => options.Value.ApiPrefix;

	protected override void Configure(ODataModelBuilder builder) {

		var User = BaseConfiguration(builder);

	}

}
