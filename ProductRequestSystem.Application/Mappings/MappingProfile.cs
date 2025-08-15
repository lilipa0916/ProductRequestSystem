using AutoMapper;
using ProductRequestSystem.Application.DTOs;
using ProductRequestSystem.Domain.Entities;
using ProductRequestSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ProductRequest, ProductRequestDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"));

            CreateMap<CreateProductRequestDto, ProductRequest>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ProductRequestStatus.Open));

            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"));

            CreateMap<CreateOfferDto, Offer>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OfferStatus.Pending));
        }
    }
}
