using AutoMapper;
using SitePet.Domain.Models;
using SitePet.Mvc.Models;

namespace SitePet.Mvc.AutoMapper
{
    public class PetAutoMappings : Profile
    {
        public PetAutoMappings()
        {
            CreateMap<Pet, PetViewModel>().ReverseMap();
        }
    }
}
