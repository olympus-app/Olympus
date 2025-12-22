namespace Olympus.Core.Backend.Identity;

public class UserQueryEndpoint : EntityQueryEndpoint<User, UserQueryRequest, UserQueryResponse, UserQueryMapper> {

	public override void Configure() {

		Get(CoreRoutes.Users.Query);

	}

}
