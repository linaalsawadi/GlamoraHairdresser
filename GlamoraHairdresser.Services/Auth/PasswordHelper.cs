using System.Security.Cryptography;

public static class PasswordHelper
{
    private const int SaltSize = 16;   // 128-bit
    private const int KeySize = 32;    // 256-bit
    private const int DefaultIterations = 100_000;
    private const int DefaultPrf = 1;  // 1 = SHA256

    // ===============================
    //     HASH PASSWORD
    // ===============================
    public static (byte[] hash, byte[] salt, int iterationCount, int prf)
        HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            DefaultIterations,
            HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(KeySize);

        return (hash, salt, DefaultIterations, DefaultPrf);
    }

    // ===============================
    //     VERIFY PASSWORD
    // ===============================
    public static bool VerifyPassword(
        string password,
        byte[] storedHash,
        byte[] storedSalt,
        int iterationCount,
        int prf)
    {
        HashAlgorithmName algo = prf switch
        {
            1 => HashAlgorithmName.SHA256,
            2 => HashAlgorithmName.SHA384,
            3 => HashAlgorithmName.SHA512,
            _ => HashAlgorithmName.SHA256
        };

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            storedSalt,
            iterationCount,
            algo);

        byte[] computedHash = pbkdf2.GetBytes(storedHash.Length);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }
}
