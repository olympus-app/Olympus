namespace Olympus.Core.Backend.Identity;

public class UserListEndpoint : EntityListEndpoint<User, UserListRequest, UserListResponse, UserListMapper> {

	public override void Configure() {

		Get(CoreRoutes.Users.Query + "list");

	}

}
