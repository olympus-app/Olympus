namespace Olympus.Core.Backend.Identity;

[Mapper]
public partial class UserListMapper : EntityListMapper<User, UserListResponse> {

	protected override partial IQueryable<UserListResponse> Map(IQueryable<User> query);

}
