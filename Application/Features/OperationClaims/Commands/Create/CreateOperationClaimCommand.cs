using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.Technologies.Rules;
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

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto> ,ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { "Admin", Admin,Write,Add };

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
    {

        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
           await _operationClaimBusinessRules.OperationClaimNameCanNotBeDuplicatedWhenInserted(request.Name);
           
            OperationClaim mappedOperationClaim =_mapper.Map<OperationClaim>(request);
           
            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);

            CreatedOperationClaimDto mappedOperationClaimDto= _mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);
            
            return mappedOperationClaimDto;

        }
    }
}
