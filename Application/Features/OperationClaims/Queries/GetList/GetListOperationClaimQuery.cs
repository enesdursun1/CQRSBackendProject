using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;


namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery:IRequest<OperationClaimListModel> ,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Read };

    public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
    {
        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> paginate = await _operationClaimRepository.GetListAsync(index:request.PageRequest.Page,
                                                                                              size:request.PageRequest.PageSize);
          
            OperationClaimListModel mappedOperationClaimList = _mapper.Map<OperationClaimListModel>(paginate); 
            
            
            return mappedOperationClaimList;
        
        }
    }

}
