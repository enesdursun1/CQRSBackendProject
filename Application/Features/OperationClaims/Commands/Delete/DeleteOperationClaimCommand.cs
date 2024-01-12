using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand:IRequest<DeletedOperationClaimDto>,ISecuredRequest
{

    public int Id { get; set; }

    public string[] Roles => new[] { "Admin",Admin,Write, OperationClaimsOperationClaims.Delete };

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {

        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim= await _operationClaimRepository.GetAsync(x=>x.Id==request.Id);
            _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
            
            _mapper.Map(request, operationClaim);


            OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);

            DeletedOperationClaimDto mappedOperationClaim = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
           
            return mappedOperationClaim;


        }
    }
}