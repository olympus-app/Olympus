namespace Olympus.Core.Backend.Identity;

public class UserReadEndpoint(UserService service) : EntityReadEndpoint<User, UserReadRequest, UserReadResponse>(service) {

	public override void Configure() {

		Get(CoreRoutes.Users.Read);

	}

}
