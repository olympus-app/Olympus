namespace Olympus.Core.Backend.Identity;

public class UserQueryEndpoint(UserService service) : EntityQueryEndpoint<User, UserQueryRequest, UserQueryResponse>(service) {

	public override void Configure() {

		Get(CoreRoutes.Users.Query);

	}

}
