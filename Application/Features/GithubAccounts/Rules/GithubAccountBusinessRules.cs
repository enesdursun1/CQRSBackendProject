using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Rules;

public class GithubAccountBusinessRules
{

    private readonly IGithubAccountRepository _githubAccountRepository;

    public GithubAccountBusinessRules(IGithubAccountRepository githubAccountRepository)
    {
        _githubAccountRepository = githubAccountRepository;
    }

    public async Task GithubAccountCanNotBeDuplicatedWhenInserted(string githubAddress)
    {
        IPaginate<GithubAccount> result = await _githubAccountRepository.GetListAsync(g => g.GithubAddress == githubAddress);
        if (result.Items.Any()) throw new BusinessException("Github Account exists");
    }
    public async Task GithubAccountCanNotBeDuplicatedWhenUpdated(string githubAddress)
    {
        IPaginate<GithubAccount> result = await _githubAccountRepository.GetListAsync(g => g.GithubAddress == githubAddress );
        if (result.Items.Any()) throw new BusinessException("Github Account exists");
    }

    public void GithubAccountShouldExistWhenSelected(GithubAccount githubAccount)
    {

        if (githubAccount == null)
            throw new BusinessException("Github Account not exists");
    }

}
