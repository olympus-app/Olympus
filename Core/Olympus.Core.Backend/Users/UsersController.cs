namespace Olympus.Core.Backend.Users;

[ApiVersion(1.0)]
[EntityController(EntityControllerActions.Read)]
public class UsersController(IEntityService<User> service) : EntityController<User, UserModel, UserMapper>(service) { }
