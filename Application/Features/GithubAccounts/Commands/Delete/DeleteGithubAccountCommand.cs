using Application.Features.GithubAccounts.Commands.Create;
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

namespace Application.Features.GithubAccounts.Commands.Delete;

public class DeleteGithubAccountCommand : IRequest<DeletedGithubAccountDto>
{
    public int Id { get; set; }


    public class DeleteGithubAccountCommandHandler : IRequestHandler<DeleteGithubAccountCommand, DeletedGithubAccountDto>
    {
        private readonly IMapper _mapper;
        private readonly IGithubAccountRepository _githubAccountRepository;
        private readonly GithubAccountBusinessRules _githubAccountBusinessRules;

        public DeleteGithubAccountCommandHandler(IMapper mapper, GithubAccountBusinessRules githubAccountBusinessRules, IGithubAccountRepository githubAccountRepository)
        {
            _mapper = mapper;
            _githubAccountBusinessRules = githubAccountBusinessRules;
            _githubAccountRepository = githubAccountRepository;
        }


        public async Task<DeletedGithubAccountDto> Handle(DeleteGithubAccountCommand request, CancellationToken cancellationToken)
        {
            GithubAccount? githubAccount = await _githubAccountRepository.GetAsync(p => p.Id == request.Id);
            _githubAccountBusinessRules.GithubAccountShouldExistWhenSelected(githubAccount);
            _mapper.Map(request, githubAccount);
            GithubAccount deletedGithubAccount = await _githubAccountRepository.DeleteAsync(githubAccount);
            DeletedGithubAccountDto mappedGithubAccount = _mapper.Map<DeletedGithubAccountDto>(deletedGithubAccount);
            return mappedGithubAccount;

        }
    }

}
