namespace Olympus.Core.Backend.Identity;

public class UserDeleteEndpoint(UserService service) : EntityDeleteEndpoint<User, UserDeleteRequest>(service) {

	public override void Configure() {

		Delete(CoreRoutes.Users.Delete);

	}

}
