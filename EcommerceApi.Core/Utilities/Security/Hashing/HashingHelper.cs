using System.Security.Cryptography;
using System.Text;

namespace EcommerceApi.Core.Utilities.Security.Hashing
{
    // HashingHelper adlı sinif, şifrəni həşləmək və şifrəni yoxlamaq üçün metodları içərir
    public static class HashingHelper
    {
        // HashPassword metodu, verilmiş şifrəni həşrləyərək həş və salt dəyərlərini əldə edir
        public static void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // HMACSHA512 həşr alqoritmasını istifadə edərək təhlükəsizlik açarını təyin edir
            using var hash = new HMACSHA512();
            // Təhlükəsizlik açarını  passwordSalt olaraq təyin edir
            passwordSalt = hash.Key;
            // Şifrəni UTF-8 formatında bayt dizinə çevirərək həşrləyir
            passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        // VerifyPassword metodu, verilmiş şifrəni əvvəlcə həşrləyib daha sonra həşr və tuz dəyərləri ilə müqayisə edir
        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Həş alqoritması ilə passwordSalt əsasında bir obyekti yaratmaq üçün HMACSHA512 istifadə edir
            using var hash = new HMACSHA512(passwordSalt);

            // Şifrəni UTF-8 formatında bayt dizinə çevirərək həşləyir
            var hashing = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Həş və salt dəyərlərini müqayisə edir
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (hashing[i] != passwordHash[i])
                    return false;
            }
            return true;
        }
    }
}
