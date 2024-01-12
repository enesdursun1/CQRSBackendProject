using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles:Profile
{

    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreatedOperationClaimDto>().ReverseMap();
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
       
        CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, DeletedOperationClaimDto>().ReverseMap();


        CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim , UpdatedOperationClaimDto>().ReverseMap();

        CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
        CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();



    }

}
