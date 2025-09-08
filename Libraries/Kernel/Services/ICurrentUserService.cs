namespace Olympus.Kernel;

public interface ICurrentUserService {

	public bool IsAuthenticated { get; }

	public AppUser UserInfo { get; }

}
