using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Technologies.Constants.TechnologiesOperationClaims;

namespace Application.Features.Technologies.Queries.GetList;

public class GetListTechnologyQuery : IRequest<TechnologyListModel>,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Read };

    public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;

        public GetListTechnologyQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
        }

        public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Technology> paginate = await _technologyRepository.GetListAsync(include:t=>t.Include(p=>p.ProgrammingLanguage),
                                                                                      index:request.PageRequest.Page,
                                                                                      size: request.PageRequest.PageSize);

            TechnologyListModel mappedtechnologyList=_mapper.Map<TechnologyListModel>(paginate);

            return mappedtechnologyList;
        }   
    }



}
