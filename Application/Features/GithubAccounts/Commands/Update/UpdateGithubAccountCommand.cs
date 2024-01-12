using Application.Features.GithubAccounts.Commands.Delete;
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

namespace Application.Features.GithubAccounts.Commands.Update;

public  class UpdateGithubAccountCommand:IRequest<UpdatedGithubAccountDto>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string GithubAddress { get; set; }

    public class UpdateGithubAccountCommandHandler : IRequestHandler<UpdateGithubAccountCommand, UpdatedGithubAccountDto>
    {
        private readonly IMapper _mapper;
        private readonly GithubAccountBusinessRules _githubAccountBusinessRules;
        private readonly IGithubAccountRepository _githubAccountRepository;

        public UpdateGithubAccountCommandHandler(IMapper mapper, GithubAccountBusinessRules githubAccountBusinessRules, IGithubAccountRepository githubAccountRepository)
        {
            _mapper = mapper;
            _githubAccountBusinessRules = githubAccountBusinessRules;
            _githubAccountRepository = githubAccountRepository;
        }


        public async Task<UpdatedGithubAccountDto> Handle(UpdateGithubAccountCommand request, CancellationToken cancellationToken)
        {
            GithubAccount? githubAccount = await _githubAccountRepository.GetAsync(p => p.Id == request.Id);
           
            _githubAccountBusinessRules.GithubAccountShouldExistWhenSelected(githubAccount);
           
            _mapper.Map(request, githubAccount);

            await _githubAccountBusinessRules.GithubAccountCanNotBeDuplicatedWhenUpdated(githubAccount.GithubAddress);
            
           GithubAccount updatedGithubAccount = await _githubAccountRepository.UpdateAsync(githubAccount);

            UpdatedGithubAccountDto mappedGithubAccount = _mapper.Map<UpdatedGithubAccountDto>(updatedGithubAccount);
           
            return mappedGithubAccount;

        }
    }
}
