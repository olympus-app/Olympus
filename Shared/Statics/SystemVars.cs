namespace Olympus.Server.Statics;

public static class SystemVars {

	// System versioning
	public static readonly int SystemBuild = 0;
	public static readonly string SystemVersion = "v0.0.0";

	// Claim name for system user identification
	public static readonly string SystemClaim = "OlympusUser";

	// Special User IDs
	public static readonly Guid UnknownUserID = new("00000000-0000-0000-0000-000000000000");
	public static readonly Guid ExternalUserID = new("00000000-0000-0000-0000-000000000001");
	public static readonly Guid ServiceUserID = new("00000000-0000-0000-0000-000000000002");
	public static readonly Guid SystemUserID = new("00000000-0000-0000-0000-000000000003");

	// Default timestamps
	public static readonly DateTime DefaultCreatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	public static readonly DateTime DefaultUpdatedAt = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

}
