using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BackendRubricas.Helper
{
    public class HashHelper
    {
        public static HashedPassword Hash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return new HashedPassword() { Password = hashed, Salt = Convert.ToBase64String(salt) };
        }

        public static bool CheckHash(string attemptedPassword, string hash, String salt) {
            String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: attemptedPassword,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hash == hashed;
        }


        public static byte[] GetHash(string password, string salt)
        {
            byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));
            SHA256 sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);
            return hashedBytes;
        }


    }

    public class HashedPassword
    {
        public string Password { get; set; }
        public string Salt { get; set; }

    }


}

