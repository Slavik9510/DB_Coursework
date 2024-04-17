using AutoMapper;
using DB_Coursework_API.Models.Domain;
using DB_Coursework_API.Models.DTO;

namespace DB_Coursework_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterCustomerDto, Customer>();
            CreateMap<Product, ProductDto>();
        }
    }
}
