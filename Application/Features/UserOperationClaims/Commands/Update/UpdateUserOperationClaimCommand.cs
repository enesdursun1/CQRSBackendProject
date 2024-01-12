using Application.Features.UserOperationClaims.Constants;
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

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, UserOperationClaimsOperationClaims.Update };

    public class
           UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<UpdatedUserOperationClaimDto> Handle(UpdateUserOperationClaimCommand request,CancellationToken cancellationToken)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(p => p.Id == request.Id);
            
            _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(userOperationClaim);
           
            _mapper.Map(request,userOperationClaim);
          UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
           
            UpdatedUserOperationClaimDto mapepdUserOperationClaim = _mapper.Map<UpdatedUserOperationClaimDto>(updatedUserOperationClaim);
            return mapepdUserOperationClaim;
        }
    }



}
