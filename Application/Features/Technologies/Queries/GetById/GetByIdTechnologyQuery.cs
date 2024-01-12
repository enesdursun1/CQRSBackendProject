using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetById;

public class GetByIdTechnologyQuery : IRequest<TechnologyGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, TechnologyGetByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public GetByIdTechnologyQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<TechnologyGetByIdDto> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
        {
            Technology mappedTechnology = _mapper.Map<Technology>(request);
            _technologyBusinessRules.TechnologyShouldExistWhenSelected(mappedTechnology);
            Technology technology = await _technologyRepository.GetAsync(predicate:p=>p.Id==mappedTechnology.Id);

            TechnologyGetByIdDto technologyGetByIdDto = _mapper.Map<TechnologyGetByIdDto>(technology);

            return technologyGetByIdDto;
        }
    }


}
