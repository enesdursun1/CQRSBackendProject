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

namespace Application.Features.GithubAccounts.Commands.Create;

public class CreateGithubAccountCommand :IRequest<CreatedGithubAccountDto>
{
    public int UserId { get; set; }
    public string GithubAddress { get; set; }

    public class CreateGithubAccountCommandHandler : IRequestHandler<CreateGithubAccountCommand, CreatedGithubAccountDto>
    {
        private readonly IMapper _mapper;
        private readonly GithubAccountBusinessRules _githubAccountBusinessRules;
        private readonly IGithubAccountRepository _githubAccountRepository;

        public CreateGithubAccountCommandHandler(IMapper mapper, GithubAccountBusinessRules githubAccountBusinessRules, IGithubAccountRepository githubAccountRepository)
        {
            _mapper = mapper;
            _githubAccountBusinessRules = githubAccountBusinessRules;
            _githubAccountRepository = githubAccountRepository;
        }

        public async Task<CreatedGithubAccountDto> Handle(CreateGithubAccountCommand request, CancellationToken cancellationToken)
        {
            await _githubAccountBusinessRules.GithubAccountCanNotBeDuplicatedWhenInserted(request.GithubAddress);

            GithubAccount githubAccount =_mapper.Map<GithubAccount>(request);
           
            GithubAccount createdGithubAccount = await _githubAccountRepository.AddAsync(githubAccount);

            CreatedGithubAccountDto mappedGithubAccount= _mapper.Map<CreatedGithubAccountDto>(createdGithubAccount);

            return mappedGithubAccount;

        }
    }
}
