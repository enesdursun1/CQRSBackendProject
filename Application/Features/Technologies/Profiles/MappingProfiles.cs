using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Commands.Delete;
using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetById;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {



        CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
        CreateMap<Technology, CreatedTechnologyDto>().ReverseMap();
       
        CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
        CreateMap<Technology, UpdatedTechnologyDto>().ReverseMap();



        CreateMap<Technology, DeleteTechnologyCommand>().ReverseMap();
        CreateMap<Technology, DeletedTechnologyDto>().ReverseMap();

        CreateMap<Technology, GetByIdTechnologyQuery>().ReverseMap();
        CreateMap<Technology, TechnologyGetByIdDto>().ReverseMap();




        CreateMap<Technology, TechnologyListDto>()
            .ForMember(t => t.ProgrammingLanguageName, opt => opt
            .MapFrom(p => p.ProgrammingLanguage.Name))
            .ReverseMap();

        CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();




    }



}
