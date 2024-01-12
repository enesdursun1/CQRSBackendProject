using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

  
    
    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p=>p.Name==name);
        if (result.Items.Any())
            throw new BusinessException("Programming Language name exists");

    }
    public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenUpdated(ProgrammingLanguage programmingLanguage)
    {
        ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => x.Id != programmingLanguage.Id && x.Name.ToLower() == programmingLanguage.Name.ToLower());
        if (result!=null)
            throw new BusinessException("Programming Language name exists");

    }
   

    public void ProgrammingLanguageShouldExistWhenSelected(ProgrammingLanguage programmingLanguage)
    {
       
        if (programmingLanguage == null)
            throw new BusinessException("Programming Language not exists");
    }
}

