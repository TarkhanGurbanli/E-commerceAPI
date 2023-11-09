using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.SpecificationDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface ISpacificationService
    {
        IResult CreateSpecification(int productId, List<SpecificationAddDTO> specificationAddDTOs);
    }
}
