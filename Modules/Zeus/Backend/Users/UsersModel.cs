namespace Olympus.Zeus.Backend;

public class UsersModel(IOptions<ZeusOptions> options) : EntityModel<User, Archend.UserModel, UsersController> {

	protected override string ModulePrefix => options.Value.ApiPrefix;

	protected override void Configure(ODataModelBuilder builder) {

		var User = BaseConfiguration(builder);

	}

}
