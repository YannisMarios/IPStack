using AutoMapper;
using IPStack.Domain.Entities;
using IPStack.WebApi.DTOs;

namespace IPStack.WebApi.Mappings
{
    public class IPDetailsMappingProfile: Profile
    {
        /// <summary>
        /// These are not neccessary and are added for demo purposes only.
        /// We could have user IPDetails Domain entity all the way.
        /// </summary>
        public IPDetailsMappingProfile()
        {
            DtoToEntity();
            EntityToDto();
        }

        private void DtoToEntity()
        {
            CreateMap<IPDetailsDTO, IPDetails>();
        }

        private void EntityToDto()
        {
            CreateMap<IPDetails, IPDetailsDTO>();
        }
    }
}
