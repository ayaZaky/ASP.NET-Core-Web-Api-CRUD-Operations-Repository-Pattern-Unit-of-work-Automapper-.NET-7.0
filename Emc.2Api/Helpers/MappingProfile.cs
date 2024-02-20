using AutoMapper;
using Emc2.Core.DtoModels;
using Emc2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emc2.Api.Helpers
{
    public class MappingProfile:Profile

    {
        public MappingProfile()
        { 
            //Service
            CreateMap<DtoService, Service>().ForMember(src => src.Icon, opt => opt.Ignore());
            CreateMap<Service, DtoServiceDetails>();
            //Client
            CreateMap<DtoClient, Client>().ForMember(src => src.Logo, opt => opt.Ignore());
            CreateMap<Client, DtoClientDetails>();
            //TeamMember
            CreateMap<DtoTeamMember, TeamMember>().ForMember(src => src.Image, opt => opt.Ignore());
            CreateMap<TeamMember, DtoTeamMemberDetails>();
            //Contact
            CreateMap<DtoContact, ContactUs>();
            CreateMap<ContactUs,DtoContactDetails>(); 
            //Industry
            CreateMap<DtoIndustry, Industry>() 
                .ForMember(src => src.Icon, opt => opt.Ignore())
                .ForMember(dest => dest.IndustryDescriptions, opt => opt.MapFrom(src => src.DescriptionLines.Select(line => new IndustryDescription { DescriptionLine = line })));
            CreateMap<Industry,DtoIndustryDetails>()
               .ForMember(dest => dest.DescriptionLines, opt => opt.MapFrom(src => src.IndustryDescriptions.Select(Desc => Desc.DescriptionLine)));
            //Product
            CreateMap<DtoProduct, Product>()
               .ForMember(src => src.Image, opt => opt.Ignore())
               .ForMember(dest => dest.ProductTechnologies, opt => opt.MapFrom(src => src.Technologies.Select(tech => new  Technology { Name = tech })));
            CreateMap<Product, DtoProductDetails>()
                .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.ProductTechnologies.Select(tech => tech.Name)));
                 
        }
         
                 
 
    }
}
