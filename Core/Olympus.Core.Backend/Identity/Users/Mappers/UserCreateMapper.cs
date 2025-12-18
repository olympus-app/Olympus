namespace Olympus.Core.Backend.Identity;

[Mapper]
public partial class UserCreateMapper : EntityCreateMapper<User, UserCreateRequest, UserReadResponse> {

	protected override partial User Map(UserCreateRequest request);

	protected override partial UserReadResponse Map(User entity);

}
