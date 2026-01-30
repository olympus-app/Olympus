namespace Olympus.Core.Backend.Identity;

public class UserService(IDatabaseService database, IHttpContextAccessor accessor) : EntityService<User>(database, accessor) { }
