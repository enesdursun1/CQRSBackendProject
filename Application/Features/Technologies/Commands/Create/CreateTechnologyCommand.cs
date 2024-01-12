using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
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
using static Application.Features.Technologies.Constants.TechnologiesOperationClaims;

namespace Application.Features.Technologies.Commands.Create;

public class CreateTechnologyCommand:IRequest<CreatedTechnologyDto>,ISecuredRequest
{
    public string Name { get; set; }
    public int ProgrammingLanguageId { get; set; }

    public string[] Roles => new[] {"Admin",Admin,Write,Add };

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public CreateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
           await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

            Technology mappedTechnology = _mapper.Map<Technology>(request);
            
            Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
           
            CreatedTechnologyDto createdTechnologyDto=_mapper.Map<CreatedTechnologyDto>(createdTechnology);

            return createdTechnologyDto;

        }
    }
}
