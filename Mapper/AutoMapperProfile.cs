using AutoMapper;
using Fletes.Models;
using Fletes.Models.DTOs;
using System;
using Route = Fletes.Models.Route;

namespace Fletes.Mapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            // Map FreightDTO to Freight
            CreateMap<FreightDTO, Freight>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Carrier, opt => opt.MapFrom(src => src.Carrier))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
                .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.Route))
                .ForMember(dest => dest.FreightProducts, opt => opt.Ignore());

            // Map CustomerDTO to Customer
            CreateMap<CustomerDTO, Customer>();

            // Map CarrierDTO to Carrier
            CreateMap<CarrierDTO, Carrier>();

            // Map VehicleDTO to Vehicle
            CreateMap<VehicleDTO, Vehicle>();

            // Map RouteDTO to Route
            CreateMap<RouteDTO, Route>();

            // Map ProductDTO to Product
            CreateMap<ProductDTO, Product>();
        }


    }
}
