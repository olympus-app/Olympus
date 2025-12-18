namespace Olympus.Core.Backend.Identity;

[Mapper]
public partial class UserReadMapper : EntityReadMapper<User, UserReadResponse> {

	protected override partial IQueryable<UserReadResponse> Map(IQueryable<User> query);

}
