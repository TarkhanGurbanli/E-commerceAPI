//Bu kod blokları, Result tasarım desenini istifadə edərək əməliyyatların nəticələrini təmsil edən bir strukturu yaradır.
//Bu struktur, əməliyyatın uğurlu olub olmadığını, bir mesajı və ehtiyac olsa bir verini əhatə edir. Həmçinin,
//uğursuz hallar üçün özəl ErrorResult və ErrorDataResult, uğurlu hallar üçün isə SuccessResult və SuccessDataResult class-ları mövcuddur.

namespace EcommerceApi.Core.Utilities.Result.Abstract
{
    // IResult interface'indən implement edilmiş bir interface
    public interface IDataResult<T> : IResult
    {
        // T Data adlı özelliğ (property), əməliyyatın nəticəsində əldə edilmiş verini təmsil edir
        T Data { get; }
    }
}
