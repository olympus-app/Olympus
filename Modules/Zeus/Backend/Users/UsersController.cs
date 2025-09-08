using UserMapper = Olympus.Zeus.Archend.UserMapper;
using UserModel = Olympus.Zeus.Archend.UserModel;

namespace Olympus.Zeus.Backend;

[ApiVersion(1.0)]
[EntityController(EntityControllerActions.All)]
public class UsersController(IEntityService<User> service) : EntityController<User, UserModel, UserMapper>(service) { }
