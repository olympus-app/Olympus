namespace Olympus.Server.Authentication;

public interface ICurrentUserService {

	public bool IsAuthenticated { get; }
	public Guid UserId { get; }

}
