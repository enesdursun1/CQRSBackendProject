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

namespace Application.Features.ProgrammingLanguages.Commands.Delete;

public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageDto>,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, ProgrammingLanguagesOperationClaims.Delete };

    public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRule;

        public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageBusinessRule)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageBusinessRule = programmingLanguageBusinessRule;
        }

        public async Task<DeletedProgrammingLanguageDto> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Id == request.Id);
            _programmingLanguageBusinessRule.ProgrammingLanguageShouldExistWhenSelected(programmingLanguage);


            _mapper.Map(request, programmingLanguage);


            ProgrammingLanguage deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(programmingLanguage);

            DeletedProgrammingLanguageDto mappedProgrammingLanguage = _mapper.Map<DeletedProgrammingLanguageDto>(deletedProgrammingLanguage);
            return mappedProgrammingLanguage;
        }
    }
}
