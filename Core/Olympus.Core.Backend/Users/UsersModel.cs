namespace Olympus.Core.Backend.Users;

public class UsersModel(IOptions<CoreModuleOptions> options) : EntityModel<User, UserModel, UsersController> {

	protected override string ModulePrefix => options.Value.ApiPrefix;

	protected override void Configure(ODataModelBuilder builder) {

		var User = BaseConfiguration(builder);

	}

}
