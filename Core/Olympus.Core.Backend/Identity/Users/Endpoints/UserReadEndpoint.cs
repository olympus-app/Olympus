namespace Olympus.Core.Backend.Identity;

public class UserReadEndpoint : EntityReadEndpoint<User, UserReadRequest, UserReadResponse, UserReadMapper> {

	public override void Configure() {

		Get(CoreRoutes.Users.Read);

	}

}
