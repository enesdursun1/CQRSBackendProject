using Application.Features.ProgrammingLanguages.Constants;
using Application.Features.ProgrammingLanguages.Dtos;
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


namespace Application.Features.ProgrammingLanguages.Commands.Update;

public class UpdateProgrammingLanguageCommand:IRequest<UpdatedProgrammingLanguageDto>,ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles =>  new[] { "Admin", Admin, Write, ProgrammingLanguagesOperationClaims.Update};

public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRule;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRule)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        }





        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
 
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(predicate: p => p.Id == request.Id);
           
          _programmingLanguageBusinessRule.ProgrammingLanguageShouldExistWhenSelected(programmingLanguage);

            _mapper.Map(request, programmingLanguage);
            await _programmingLanguageBusinessRule.ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(programmingLanguage);
            

            ProgrammingLanguage updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);
            UpdatedProgrammingLanguageDto mappedProgrammingLanguage = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

            return mappedProgrammingLanguage;
        }

        
    }


}
