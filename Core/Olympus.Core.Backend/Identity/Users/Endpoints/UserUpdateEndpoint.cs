namespace Olympus.Core.Backend.Identity;

public class UserUpdateEndpoint(UserService service) : EntityUpdateEndpoint<User, UserUpdateRequest, UserReadResponse>(service) {

	public override void Configure() {

		Put(CoreRoutes.Users.Update);

	}

}
