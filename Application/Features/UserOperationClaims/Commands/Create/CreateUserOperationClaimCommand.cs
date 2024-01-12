using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;


namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>,ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, Add };

    public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
      

        public CreateUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _mapper = mapper;
            _userOperationClaimRepository = userOperationClaimRepository;
           
        }

        public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            UserOperationClaim mapepdUserOperationClaim = _mapper.Map<UserOperationClaim>(request);


            UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mapepdUserOperationClaim);

           CreatedUserOperationClaimDto mappedUserOperationClaimDto= _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);
        
        return mappedUserOperationClaimDto;
        }
    }
    
  }
