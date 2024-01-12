using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
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

namespace Application.Features.Technologies.Commands.Delete;

public class DeleteTechnologyCommand:IRequest<DeletedTechnologyDto>,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { "Admin", Admin, Write, TechnologiesOperationClaims.Delete };

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
    {


        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public DeleteTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository, TechnologyBusinessRules technologyBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }


        public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology? technology = await _technologyRepository.GetAsync(p => p.Id == request.Id);
            
            _technologyBusinessRules.TechnologyShouldExistWhenSelected(technology);

            _mapper.Map(request, technology);


            Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);

            DeletedTechnologyDto mappedTechnology = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
            return mappedTechnology;
        }
    }
}
