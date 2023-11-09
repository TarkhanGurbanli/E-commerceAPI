using Microsoft.Extensions.Configuration;

//Microsoft.Extensions.Configuration bu Nugeti ve Microsoft.Extensions.Configuration.Json yuklemek lazimdir,
//Burada WebApi icindeki appsettings.jso icindeki melumatlari cekirik

namespace EcommerceApi.Core.Configuration
{
    // DatabaseConfiguration adlı sinif, uygulamanın veritabanına bağlanma konfigurasiyasını təyin edir
    public static class DatabaseConfiguration
    {
        // ConnectionString adlı özelliq, veritabanına bağlanmaq üçün konfiqurasiya parametrlərini təyin edir
        public static string ConnectionString
        {
            get
            {
                // ConfigurationManager obyekti yaratmaq
                ConfigurationManager configurationManager = new();
                // Uygulamanın temel direktoriyasını təyin edib əlavə etmək
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EcommerceApi.WebApi"));
                // appsettings.json faylından konfiqurasiya parametrlərini əldə etmək
                configurationManager.AddJsonFile("appsettings.json");
                // "Default" adlı bağlantı parametrini əldə etmək və geri qaytarmaq
                return configurationManager.GetConnectionString("Default");
            }
        }
    }
}
