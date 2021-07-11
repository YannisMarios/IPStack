using AutoMapper;
using IPStack.Domain.Entities;
using IPStack.Adapter.Model;

namespace IPStack.Adapter.Mappings
{
    public class IPInfoProviderMappingProfile: Profile
    {
        public IPInfoProviderMappingProfile()
        {
            InfrastructureToEntity();
        }

        private void InfrastructureToEntity()
        {
            _ = CreateMap<ApiResponse, IPDetails>()
                .ForMember(dest => dest.Id, src => src.Ignore());
        }
    }
}
