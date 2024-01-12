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

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand:IRequest<DeletedUserOperationClaimDto>,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, UserOperationClaimsOperationClaims.Delete };

    public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _mapper = mapper;
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }



        public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
        {

            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(p => p.Id == request.Id);
            
             _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(userOperationClaim);

            _mapper.Map(request, userOperationClaim);

            UserOperationClaim deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
           
            DeletedUserOperationClaimDto mappedUserOperationClaimDto = _mapper.Map<DeletedUserOperationClaimDto>(deletedUserOperationClaim);

            return mappedUserOperationClaimDto;
        }
    }

}
