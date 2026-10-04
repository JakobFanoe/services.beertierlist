using Microsoft.AspNetCore.Identity;

namespace services.beertierlist.application.Commands.RegisterAccount;

public class RegisterAccountCommandHandler(UserManager<IdentityUser> userManager) : IRegisterAccountCommandHandler
{
    public async Task<RegisterAccountResult> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByNameAsync(command.Username);
        if (existingUser is not null)
            return new RegisterAccountResult(RegisterAccountStatus.UsernameUnavailable);

        var user = new IdentityUser { UserName = command.Username, LockoutEnabled = true };
        var result = await userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
            return new RegisterAccountResult(RegisterAccountStatus.Created, user.Id, user.UserName);

        if (result.Errors.Any(error => error.Code == "DuplicateUserName"))
            return new RegisterAccountResult(RegisterAccountStatus.UsernameUnavailable);

        return new RegisterAccountResult(RegisterAccountStatus.Failed);
    }
}