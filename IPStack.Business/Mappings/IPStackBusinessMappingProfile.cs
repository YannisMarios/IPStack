using AutoMapper;
using IPStack.Domain.Entities;

namespace IPStack.Business.Mappings
{
    public class IPStackBusinessMappingProfile : Profile
    {
        public IPStackBusinessMappingProfile()
        {
            EntityToEntity();
        }

        private void EntityToEntity()
        {
            CreateMap<IPDetails, IPDetails>();
        }
    }
}
