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

namespace Application.Features.UserOperationClaims.Rules;
public class UserOperationClaimBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

   

    public void UserOperationClaimIdShouldExistWhenSelected(UserOperationClaim userOperationClaim)
    {

        if (userOperationClaim == null)
            throw new BusinessException("User operation claim not exists");
    }
}


