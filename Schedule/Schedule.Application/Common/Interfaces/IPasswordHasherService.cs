namespace Schedule.Application.Common.Interfaces;

public interface IPasswordHasherService
{
    string Hash(string password);
    bool EnhancedHash(string value, string hash);
}