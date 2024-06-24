using AutoMapper;
using UdemyWebApi.Models.Domain;
using UdemyWebApi.Models.DTO;

namespace UdemyWebApi.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<Source, Destination>;
            CreateMap<Region, RegionDto>().ReverseMap(); ;
            CreateMap<Region, CreateRegionDto>().ReverseMap(); ;
            CreateMap<Region, UpdateRegionDto>().ReverseMap(); ;
        }
    }
}
