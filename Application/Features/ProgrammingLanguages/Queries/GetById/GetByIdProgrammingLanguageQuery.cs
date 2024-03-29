﻿using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace Application.Features.ProgrammingLanguages.Queries.GetById;

public class GetByIdProgrammingLanguageQuery:IRequest<ProgrammingLanguageGetByIdDto>,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles =>  new[] { "Admin", Admin, Read };

    public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, ProgrammingLanguageGetByIdDto>
    {
       private readonly IMapper _mapper;
       private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
       private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

        public GetByIdProgrammingLanguageQueryHandler(IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
        {
            _mapper = mapper;
            _programmingLanguageRepository = programmingLanguageRepository;
            _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
        }

        public async Task<ProgrammingLanguageGetByIdDto> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
          ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(p=>p.Id==request.Id);

          _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenSelected(programmingLanguage);

            ProgrammingLanguageGetByIdDto mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguageGetByIdDto>(programmingLanguage);

            return mappedProgrammingLanguage;



        }
    }


}
