namespace Olympus.Core.Backend.Identity;

[Mapper]
public partial class UserQueryMapper : EntityQueryMapper<User, UserQueryResponse> {

	protected override partial IQueryable<UserQueryResponse> Map(IQueryable<User> query);

}
