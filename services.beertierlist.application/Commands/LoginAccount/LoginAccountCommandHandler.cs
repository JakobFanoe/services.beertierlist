using Microsoft.AspNetCore.Identity;
using services.beertierlist.application.Services;

namespace services.beertierlist.application.Commands.LoginAccount;

public class LoginAccountCommandHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IAccessTokenGenerator accessTokenGenerator) : ILoginAccountCommandHandler
{
    public async Task<string?> Handle(LoginAccountCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(command.Username);
        if (user is null)
            return null;

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: true);
        if (!signInResult.Succeeded)
            return null;

        return accessTokenGenerator.Generate(user.Id, user.UserName ?? string.Empty);
    }
}