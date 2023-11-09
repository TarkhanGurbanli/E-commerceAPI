using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.SpecificationDTOs;

namespace EcommerceApi.Business.Concrete
{
    public class SpacificationManager : ISpacificationService
    {
        private readonly ISpacificationDAL _specificationDAL;
        private readonly IMapper _mapper;
        public SpacificationManager(IMapper mapper, ISpacificationDAL specificationDAL)
        {
            _mapper = mapper;
            _specificationDAL = specificationDAL;
        }

        public IResult CreateSpecification(int productId, List<SpecificationAddDTO> specificationAddDTOs)
        {
            var map = _mapper.Map<List<Spacification>>(specificationAddDTOs);
            _specificationDAL.AddSpecification(productId, map);
            return new SuccessResult();
        }
    }
}
