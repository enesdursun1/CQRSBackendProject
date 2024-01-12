using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Constants;
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

namespace Application.Features.Technologies.Commands.Update;

public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto> ,ISecuredRequest
{
    public int Id { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, TechnologiesOperationClaims.Update };

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
    {

        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public UpdateTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology? technology= await _technologyRepository.GetAsync(predicate: p => p.Id == request.Id);

            _technologyBusinessRules.TechnologyShouldExistWhenSelected(technology);

            _mapper.Map(request, technology);
            await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenUpdated(technology);

            Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);

            UpdatedTechnologyDto mappedTechnology=_mapper.Map<UpdatedTechnologyDto>(updatedTechnology);

            return mappedTechnology;
        }
    }
}
