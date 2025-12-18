namespace Olympus.Core.Backend.Identity;

public class UserDeleteEndpoint : EntityDeleteEndpoint<User, UserDeleteRequest> {

	public override void Configure() {

		Delete(CoreRoutes.Users.Delete);

	}

}
