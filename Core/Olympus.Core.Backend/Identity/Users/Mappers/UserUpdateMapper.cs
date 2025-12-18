namespace Olympus.Core.Backend.Identity;

[Mapper]
public partial class UserUpdateMapper : EntityUpdateMapper<User, UserUpdateRequest, UserReadResponse> {

	protected override partial void Map(UserUpdateRequest request, User entity);

	protected override partial UserReadResponse Map(User entity);

}
