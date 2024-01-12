using Application.Features.ProgrammingLanguages.Commands.Create;
using Application.Features.ProgrammingLanguages.Commands.Delete;
using Application.Features.ProgrammingLanguages.Commands.Update;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetById;
using Application.Features.ProgrammingLanguages.Queries.GetList;
using Application.Features.Technologies.Commands.Create;
using Application.Features.Technologies.Commands.Delete;
using Application.Features.Technologies.Commands.Update;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetById;
using Application.Features.Technologies.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);
            return Created("", result);
        }
       
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };
            TechnologyListModel result = await Mediator.Send(getListTechnologyQuery);
            return Ok(result);
        }
       

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Ok(result);
        }


      

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);
            return Ok(result);
        }
       
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery)
        {
            TechnologyGetByIdDto result = await Mediator.Send(getByIdTechnologyQuery);
            return Ok(result);
        }


    }
}
