using EcommerceApi.Core.Utilities.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Api.NUnitTest
{
    // PasswordTest adlı sinif, HashingHelper sinifinin metodlarını yoxlamaq üçün testləri icra edir
    [TestFixture]
    public class PasswordTest
    {
        // CheckUserPassword metodu, düzgün şifrə ilə yaratılan həşr və tuzun doğruluğunu yoxlamaq üçün nümunə testdir
        [Test]
        public void CheckUserPassword()
        {
            // "tarxan123" şifrəsi üçün həş və salt yaradılması
            HashingHelper.HashPassword("tarxan123", out byte[] passwordHash, out byte[] passwordSalt);
            // Şifrəni doğrulamaq üçün VerifyPassword metodunu çağırmaq
            var data = HashingHelper.VerifyPassword("tarxan123", passwordHash, passwordSalt);
            // Doğrulama neticəsini yoxlamaq üçün NUnit Assert sınıfının True metodunu istifadə etmək
            Assert.True(data);
        }
        // CheckUserWrongPassword metodu, yanlış şifrə ilə yaratılan həşr və tuzun doğruluğunu yoxlamaq üçün nümunə testdir
        [Test]
        public void CheckUserWrongPassword()
        {
            // "tarxan123" şifrəsi üçün həş və salt yaradılması
            HashingHelper.HashPassword("tarxan123", out byte[] passwordHash, out byte[] passwordSalt);
            // Yanlış şifrəni doğrulamaq üçün VerifyPassword metodunu çağırmaq
            var data = HashingHelper.VerifyPassword("tarxan1243", passwordHash, passwordSalt);
            // Doğrulama neticəsini yoxlamaq üçün NUnit Assert sınıfının False metodunu istifadə etmək
            Assert.False(data);
        }
    }
}
