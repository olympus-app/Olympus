namespace Olympus.Core.Backend.Identity;

public class UserCreateEndpoint(UserService service) : EntityCreateEndpoint<User, UserCreateRequest, UserReadResponse>(service) {

	public override void Configure() {

		Post(CoreRoutes.Users.Create);

	}

}
