namespace services.beertierlist.application.Services;

public interface IAccessTokenGenerator
{
    string Generate(string userId, string username);
}