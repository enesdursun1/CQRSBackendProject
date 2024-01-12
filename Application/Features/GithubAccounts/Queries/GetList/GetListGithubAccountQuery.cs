using Application.Features.GithubAccounts.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Queries.GetList;

public class GetListGithubAccountQuery:IRequest<GithubAccountListModel>
{
    public PageRequest PageRequest { get; set; }


    public class GetListGithubAccountQueryHandler : IRequestHandler<GetListGithubAccountQuery, GithubAccountListModel>
    {
        private readonly IMapper _mapper;
        private readonly IGithubAccountRepository _githubAccountRepository;

        public GetListGithubAccountQueryHandler(IMapper mapper, IGithubAccountRepository githubAccountRepository)
        {
            _mapper = mapper;
            _githubAccountRepository = githubAccountRepository;
        }

        public async Task<GithubAccountListModel> Handle(GetListGithubAccountQuery request, CancellationToken cancellationToken)
        {
            IPaginate<GithubAccount> paginate = await _githubAccountRepository.GetListAsync();

            GithubAccountListModel mappedGithubAccountList = _mapper.Map<GithubAccountListModel>(paginate);
           
            return mappedGithubAccountList;
        }
    }
}
