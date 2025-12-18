namespace Olympus.Core.Backend.Identity;

public class UserCreateEndpoint : EntityCreateEndpoint<User, UserCreateRequest, UserReadResponse, UserCreateMapper> {

	public override void Configure() {

		Post(CoreRoutes.Users.Create);

	}

}
