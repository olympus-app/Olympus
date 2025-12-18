namespace Olympus.Core.Backend.Identity;

public class UserUpdateEndpoint : EntityUpdateEndpoint<User, UserUpdateRequest, UserReadResponse, UserUpdateMapper> {

	public override void Configure() {

		Put(CoreRoutes.Users.Update);

	}

}
