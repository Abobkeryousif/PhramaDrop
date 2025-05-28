using AutoMapper;
using PharmaDrop.Aplication.DTOs;
using PharmaDrop.Application.DTOs;
using PharmaDrop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Application.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            //User Mappping
            CreateMap<User , UserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();

            //Product Mapping
            CreateMap<ProductDto, Product>().ForMember(p => p.photos, option => option.Ignore());
            CreateMap<UpdateProductDto, Product>().ForMember(p => p.photos, option => option.Ignore());
        }
    }
}
