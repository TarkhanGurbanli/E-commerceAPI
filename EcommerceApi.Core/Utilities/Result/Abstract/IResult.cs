//Bu kod blokları, Result tasarım desenini istifadə edərək əməliyyatların nəticələrini təmsil edən bir strukturu yaradır.
//Bu struktur, əməliyyatın uğurlu olub olmadığını, bir mesajı və ehtiyac olsa bir verini əhatə edir. Həmçinin,
//uğursuz hallar üçün özəl ErrorResult və ErrorDataResult, uğurlu hallar üçün isə SuccessResult və SuccessDataResult class-ları mövcuddur.

namespace EcommerceApi.Core.Utilities.Result.Abstract
{
    // IResult adlı bir interface, əməliyyatın uğurlu olub olmadığını və bir mesajın olması lazım olduğunu göstərir
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
