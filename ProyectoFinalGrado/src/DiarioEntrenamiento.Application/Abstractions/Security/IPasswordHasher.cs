namespace DiarioEntrenamiento.Application.Abstractions.Security;
// implementacion en infrastructure https://www.youtube.com/watch?v=J4ix8Mhi3rs
public interface IPasswordHasher
{
    string HashPassword(string ContrasenaPlana);
    bool Verify(string ContrasenaPlana, string ContrasenaHash);
}