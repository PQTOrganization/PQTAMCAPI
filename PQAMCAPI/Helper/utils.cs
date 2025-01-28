using System;
using System.Text;
using System.Security.Cryptography;


namespace PQAMCAPI.Helper
{
    public class utils
    {
        public static byte [] EncryptPassword(string password)
        {
            byte[] salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);

            // Convert the plain string pwd into bytes
            byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(password);
            // Append salt to pwd before hashing
            byte[] combinedBytes = new byte[plainTextBytes.Length + salt.Length];
            Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
            Buffer.BlockCopy(salt, 0, combinedBytes, plainTextBytes.Length, salt.Length);

            // Create hash for the pwd+salt
            HashAlgorithm hashAlgo = SHA256.Create();  //SHA256Managed();
            byte[] hash = hashAlgo.ComputeHash(combinedBytes);

            // Append the salt to the hash
            byte[] hashPlusSalt = new byte[hash.Length + salt.Length];
            Buffer.BlockCopy(hash, 0, hashPlusSalt, 0, hash.Length);
            Buffer.BlockCopy(salt, 0, hashPlusSalt, hash.Length, salt.Length);
            return hashPlusSalt;
        }

        public static bool ComparePassword(string newPassword, byte[] hashedPwd)
        {
            byte[] salt = new byte[32];
            Buffer.BlockCopy(hashedPwd, hashedPwd.Length-32, salt, 0, salt.Length);

            // Convert the plain string pwd into bytes
            byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(newPassword);
            // Append salt to pwd before hashing
            byte[] combinedBytes = new byte[plainTextBytes.Length + salt.Length];
            Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
            Buffer.BlockCopy(salt, 0, combinedBytes, plainTextBytes.Length, salt.Length);

            // Create hash for the pwd+salt
            HashAlgorithm hashAlgo = new SHA256Managed();
            byte[] hash = hashAlgo.ComputeHash(combinedBytes);

            // Append the salt to the hash
            byte[] hashPlusSalt = new byte[hash.Length + salt.Length];
            Buffer.BlockCopy(hash, 0, hashPlusSalt, 0, hash.Length);
            Buffer.BlockCopy(salt, 0, hashPlusSalt, hash.Length, salt.Length);
            return hashPlusSalt.SequenceEqual(hashedPwd);
        }
    }
}
