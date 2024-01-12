using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules;
public class OperationClaimBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task OperationClaimNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<OperationClaim> result = await _operationClaimRepository.GetListAsync(p => p.Name == name);
        if (result.Items.Any())
            throw new BusinessException("Operation Claim name exists");

    }
    public async Task OperationClaimNameCanNotBeDuplicatedWhenUpdated(OperationClaim operationClaim)
    {
        OperationClaim? result = await _operationClaimRepository.GetAsync(x => x.Id != operationClaim.Id && x.Name.ToLower() == operationClaim.Name.ToLower());
        if (result != null)
            throw new BusinessException("Operation Claim name exists");

    }


    public void OperationClaimShouldExistWhenSelected(OperationClaim operationClaim)
    {

        if (operationClaim == null)
            throw new BusinessException("Operation Claim not exists");
    }


}
