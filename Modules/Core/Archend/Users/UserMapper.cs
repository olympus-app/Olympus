namespace Olympus.Core.Archend;

[Mapper]
public partial class UserMapper : IMapper<User, UserModel> {

	public static partial User MapFrom(UserModel model);

	public static partial UserModel MapFrom(User entity);

	public static partial void MapTo([MappingTarget] User entity, UserModel model);

	public static partial void MapTo([MappingTarget] UserModel model, User entity);

	public static partial IQueryable<UserModel> ProjectFrom(IQueryable<User> query);

	public static partial IQueryable<User> ProjectFrom(IQueryable<UserModel> query);

}
