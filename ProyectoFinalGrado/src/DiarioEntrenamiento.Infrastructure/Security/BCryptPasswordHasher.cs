using System.Security.Cryptography;
using DiarioEntrenamiento.Application.Abstractions.Security;

namespace DiarioEntrenamiento.Infrastructure.Security;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;
    public string HashPassword(string ContrasenaPlana)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(ContrasenaPlana, salt, Iterations, algorithm, HashSize);
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

        
    }

    public bool Verify(string ContrasenaPlana, string ContrasenaHash)
    {
        string[] parts = ContrasenaHash.Split('-');
        byte [] hash=Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(ContrasenaPlana, salt, Iterations, algorithm, HashSize);
        // return hash.SequenceEqual(inputHash);
        return CryptographicOperations.FixedTimeEquals(hash, inputHash); // mas seguro
    }
}