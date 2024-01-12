using Application.Features.ProgrammingLanguages.Constants;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace Application.Features.ProgrammingLanguages.Queries.GetList;

public class GetListProgrammingLanguageQuery:IRequest<ProgrammingLanguageListModel>,ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Read };

    public class GetListProgrammingLanguageQueryHandler:IRequestHandler<GetListProgrammingLanguageQuery, ProgrammingLanguageListModel>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            
        }

        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguage> programmingLanguages = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            ProgrammingLanguageListModel mappedprogrammingLanguage = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);
           
            return mappedprogrammingLanguage;
        }
    }

}
