using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQuery : IRequest<UserOperationClaimListModel>,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Read };

    public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, UserOperationClaimListModel>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _mapper = mapper;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> paginate = await _userOperationClaimRepository.GetListAsync(include:p=>p.Include(a=>a.OperationClaim),
                                                                                                      index: request.PageRequest.Page,
                                                                                                      size: request.PageRequest.PageSize);

            UserOperationClaimListModel mappeduserOperationClaim = _mapper.Map<UserOperationClaimListModel>(paginate);

            return mappeduserOperationClaim;

        }
    }

}
