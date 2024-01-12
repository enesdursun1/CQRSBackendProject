using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace Application.Features.ProgrammingLanguages.Commands.Create;

public class CreateProgrammingLanguageCommand :IRequest<CreatedProgrammingLanguageDto> ,ISecuredRequest
{

    public string Name { get; set; }
    public string[] Roles => new[] { "Admin",Admin,Write,Add };


    public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageDto>
    {
       private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
       private readonly IMapper _mapper;
       private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRule;

        public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRule)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        }

        public async Task<CreatedProgrammingLanguageDto> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _programmingLanguageBusinessRule.ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(request.Name);

            ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request);
            ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);
            CreatedProgrammingLanguageDto mappedProgrammingLanguageDto = _mapper.Map<CreatedProgrammingLanguageDto>(createdProgrammingLanguage);
            
            return mappedProgrammingLanguageDto;

        }
    }
}
