using EcommerceApi.Core.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Core.Utilities.Security.JWT
{
    // Token adlı sinif, JWT (Json Web Token) yaratmaq üçün istifadə olunur
    public static class Token
    {
        // TokenGenerator metod, istifadəçi məlumatları və rol parametrini qəbul edərək bir JWT yaradır
        public static string TokenGenerator(AppUser user, string role)
        {
            // JWT tokenını idarə etmək üçün JwtSecurityTokenHandler sinifi istifadə olunur
            var jwtHandler = new JwtSecurityTokenHandler();
            // JWT-nin imzalanması üçün istifadə olunacaq gizli açar təyin olunur
            var key = Encoding.UTF8.GetBytes("nmDLKAna9f9WEKPPH7z3tgwnQ433FAtrdP5c9AmDnmuJp9rzwTPwJ9yUu");
            // JWT tokenının xüsusiyyətlərini təyin edən bir SecurityTokenDescriptor yaradılır
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Token məzmunu, istifadəçi məlumatları və rol ilə təyin olunan bir ClaimsIdentity obyekti ilə təyin edilir
                Subject = new ClaimsIdentity(new[]
                 {
             new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
             new Claim(JwtRegisteredClaimNames.Email, user.Email),
             new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim ("Admin", role),
         }),
                // Tokenın keçərlilik müddəti təyin olunur (hazırda 10 dəqiqə)
                Expires = DateTime.UtcNow.AddDays(1),
                // Tokenın imzalanması üçün istifadə olunacaq alqoritma və açar təyin olunur
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                // Tokenın yaradan (Issuer) və istifadə olunacaq məqsəd (Audience) təyin olunur
                Issuer = "ComparAcademy",
                Audience = "ComparAcademy"
            };
            // Yukarıda təyin olunan xüsusiyyətlərə sahib bir JWT tokenı yaradılır
            var token = jwtHandler.CreateToken(tokenDescriptor);
            // Yaradılan token bir stringə çevrilərək qaytarılır
            return jwtHandler.WriteToken(token);
        }
    }
}
