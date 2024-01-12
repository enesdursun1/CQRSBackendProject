using Application.Features.GithubAccounts.Commands.Create;
using Application.Features.GithubAccounts.Commands.Delete;
using Application.Features.GithubAccounts.Commands.Update;
using Application.Features.GithubAccounts.Dtos;
using Application.Features.GithubAccounts.Models;
using Application.Features.GithubAccounts.Queries.GetById;
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

namespace Application.Features.GithubAccounts.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {



        CreateMap<GithubAccount, CreateGithubAccountCommand>().ReverseMap();
        CreateMap<GithubAccount, CreatedGithubAccountDto>().ReverseMap();

        CreateMap<GithubAccount, UpdateGithubAccountCommand>().ReverseMap();
        CreateMap<GithubAccount, UpdatedGithubAccountDto>().ReverseMap();



        CreateMap<GithubAccount, DeleteGithubAccountCommand>().ReverseMap();
        CreateMap<GithubAccount, DeletedGithubAccountDto>().ReverseMap();
       
        CreateMap<IPaginate<GithubAccount>, GithubAccountListModel>().ReverseMap();
        CreateMap<GithubAccount, GithubAccountListDto>().ReverseMap();

       
        
        CreateMap<GithubAccount, GetByIdGithubAccountQuery>().ReverseMap();
        CreateMap<GithubAccount, GithubAccountGetByIdDto>().ReverseMap();








    }


}
