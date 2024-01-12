using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules;

public class TechnologyBusinessRules
{

    private readonly ITechnologyRepository _technologyRepository;

    public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }





    public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Technology> result = await _technologyRepository.GetListAsync(p => p.Name == name);
        if (result.Items.Any())
            throw new BusinessException("Technology name exists");

    }
    public async Task TechnologyNameCanNotBeDuplicatedWhenUpdated(Technology technology)
    {
        Technology? result = await _technologyRepository.GetAsync(x => x.Id != technology.Id && x.Name.ToLower() == technology.Name.ToLower());
        if (result != null)
            throw new BusinessException("Technology name exists");

    }


    public void TechnologyShouldExistWhenSelected(Technology technology)
    {

        if (technology == null)
            throw new BusinessException("Technology not exists");
    }
}
