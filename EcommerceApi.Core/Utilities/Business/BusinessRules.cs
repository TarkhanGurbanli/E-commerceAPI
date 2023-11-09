using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;

//BusinessRules sinifinin Check metodu, bir dizi iş əməliyyat obyektini (logics) qəbul edir
//və hər birinin uğurlu olub olmadığını yoxlayır. Əgər hər hansı bir iş əməliyyatı uğursuz olduysa,
//ErrorResult obyekti qaytarılır; əks halda, SuccessResult obyekti qaytarılır.
//Bu, bir sıra iş qaydalarının bir araya gətirilərək toplu şəkildə yoxlanılmasına imkan verir

namespace EcommerceApi.Core.Utilities.Business
{
    // BusinessRules adlı bir sinif təyin edilir
    public class BusinessRules
    {
        // Check adlı bir metod təyin edilir, bu metod IResult növündə parametrləri qəbul edir
        public static IResult Check(params IResult[] logics)
        {
            // logics adlı IResult növündəki massivi foreach dövrü ilə yoxlayır
            foreach (var logic in logics)
            {
                // Hər bir logic (iş əməliyyatı) obyektinin Success xüsusiyyəti yoxlanılır
                if (!logic.Success)
                    // Əgər Success xüsusiyyəti false-dürsə, yeni bir ErrorResult obyekti qaytarılır
                    return new ErrorResult();
            }
            // Əgər bütün iş əməliyyatları (logics) uğurlu olduysa, yeni bir SuccessResult obyekti qaytarılır
            return new SuccessResult();
        }
    }
}
