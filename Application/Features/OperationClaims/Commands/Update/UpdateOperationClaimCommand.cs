using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
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
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimDto>,ISecuredRequest
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, OperationClaimsOperationClaims.Update };

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
    {

        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public UpdateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }



        public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
            _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            _mapper.Map(request, operationClaim);

           await  _operationClaimBusinessRules.OperationClaimNameCanNotBeDuplicatedWhenUpdated(operationClaim);
           
            OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
           
            UpdatedOperationClaimDto mappedOperationClaimDto = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);
            
            return mappedOperationClaimDto;
        }
    }
}