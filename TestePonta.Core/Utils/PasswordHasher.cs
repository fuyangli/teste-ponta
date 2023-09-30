using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            string saltedPassword = string.Concat(password, salt);

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string hashedPassword, string password, string salt)
    {
        string expectedHash = HashPassword(password, salt);
        return string.Equals(hashedPassword, expectedHash, StringComparison.OrdinalIgnoreCase);
    }

    public static string GenerateSalt()
    {
        byte[] randomBytes = RandomNumberGenerator.GetBytes(16);
        RandomNumberGenerator.GetBytes(16);
        return Convert.ToBase64String(randomBytes);
    }
}

//public class Program
//{
//    public static void Main()
//    {
//        // Exemplo de como usar o PasswordHasher
//        string password = "senhaSegura";
//        string salt = GenerateSalt(); // Gere um salt aleatório para cada usuário

//        string hashedPassword = PasswordHasher.HashPassword(password, salt);

//        Console.WriteLine($"Senha Hashed: {hashedPassword}");

//        bool isPasswordValid = PasswordHasher.VerifyPassword(hashedPassword, password, salt);
//        Console.WriteLine($"A senha é válida? {isPasswordValid}");
//    }

    
//}
