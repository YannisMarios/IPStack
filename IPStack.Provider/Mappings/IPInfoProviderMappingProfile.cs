using AutoMapper;
using IPStack.Domain.Entities;
using IPStack.Adapter.Model;

namespace IPStack.Adapter.Mappings
{
    public class IPInfoProviderMappingProfile: Profile
    {
        public IPInfoProviderMappingProfile()
        {
            InfrastructureToDomain();
        }

        private void InfrastructureToDomain()
        {
            _ = CreateMap<ApiResponse, IPDetails>()
                .ForMember(dest => dest.Id, src => src.Ignore());
        }
    }
}
