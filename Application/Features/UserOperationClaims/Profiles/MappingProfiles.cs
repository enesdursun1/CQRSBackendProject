using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries.GetById;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles:Profile
{
	public MappingProfiles()
	{
		CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
		CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();

        CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();

        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();

        CreateMap<UserOperationClaim, GetByIdUserOperationClaimQuery>().ReverseMap();
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimDto>().ReverseMap();
      



        CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
        CreateMap<UserOperationClaim, GetListUserOperationClaimDto>()
            .ForMember(p=>p.OperationClaimName, opt => opt
            .MapFrom(a=>a.OperationClaim.Name))
            .ReverseMap();
    }

}
