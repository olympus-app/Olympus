using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyRegisterOptionsEndpoint : Endpoint {

	public UserManager<User> UserManager { get; set; } = default!;

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Passkeys.RegisterOptions);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(CancellationToken cancellationToken) {

		var user = await UserManager.GetUserAsync(User);

		if (user is null) return await Send.NotFoundAsync(ErrorsStrings.Values.UserInactiveOrNotFound, cancellationToken);

		var passkeyUser = new PasskeyUserEntity() {
			Id = await UserManager.GetUserIdAsync(user),
			Name = await UserManager.GetUserNameAsync(user) ?? AppUsers.Unknown.Name,
			DisplayName = user.Name,
		};

		try {

			var optionsJson = await SignInManager.MakePasskeyCreationOptionsAsync(passkeyUser);

			return await Send.StringAsync(optionsJson, ContentTypes.Json, cancellationToken);

		} catch (Exception exception) {

			return await Send.ExceptionAsync(exception, cancellationToken);

		}

	}

}
