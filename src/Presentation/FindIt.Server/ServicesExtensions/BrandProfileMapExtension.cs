using AutoMapper;
using FindIt.Domain.ProductEntities;
using FindIt.Shared.DTOs;

namespace FindIt.Server.ServicesExtensions
{
    public class BrandProfileMapExtension:Profile
    {
        public BrandProfileMapExtension()
        {
            CreateMap<BrandRequest,Brand>();
            CreateMap<BrandResponse,Brand>();   
        }


    }
}