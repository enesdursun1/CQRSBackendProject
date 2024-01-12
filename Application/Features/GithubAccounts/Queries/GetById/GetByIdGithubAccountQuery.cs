using Application.Features.GithubAccounts.Dtos;
using Application.Features.GithubAccounts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Queries.GetById;

public class GetByIdGithubAccountQuery:IRequest<GithubAccountGetByIdDto>
{
    public int Id { get; set; }


    public class GetByIdGithubAccountQueryHandler : IRequestHandler<GetByIdGithubAccountQuery, GithubAccountGetByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly GithubAccountBusinessRules _githubAccountBusinessRules;
        private readonly IGithubAccountRepository _githubAccountRepository;

        public GetByIdGithubAccountQueryHandler(IMapper mapper, GithubAccountBusinessRules githubAccountBusinessRules, IGithubAccountRepository githubAccountRepository)
        {
            _mapper = mapper;
            _githubAccountBusinessRules = githubAccountBusinessRules;
            _githubAccountRepository = githubAccountRepository;
        }

        public async Task<GithubAccountGetByIdDto> Handle(GetByIdGithubAccountQuery request, CancellationToken cancellationToken)
        {
            GithubAccount mappedGithubAccount = _mapper.Map<GithubAccount>(request);

           _githubAccountBusinessRules.GithubAccountShouldExistWhenSelected(mappedGithubAccount);

            GithubAccount githubAccount = await _githubAccountRepository.GetAsync(predicate:p=>p.Id== mappedGithubAccount.Id);

            GithubAccountGetByIdDto githubAccountGetByIdDto = _mapper.Map<GithubAccountGetByIdDto>(githubAccount);
          
            return githubAccountGetByIdDto;
        }
    }

}
