using Application.Features.GithubAccounts.Commands.Create;
using Application.Features.GithubAccounts.Commands.Delete;
using Application.Features.GithubAccounts.Commands.Update;
using Application.Features.GithubAccounts.Dtos;
using Application.Features.GithubAccounts.Models;
using Application.Features.GithubAccounts.Queries.GetById;
using Application.Features.GithubAccounts.Queries.GetList;
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
    public class GithubAccountsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubAccountCommand createGithubAccountCommand)
        {
            CreatedGithubAccountDto result = await Mediator.Send(createGithubAccountCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubAccountQuery getListGithubAccountQuery = new() { PageRequest = pageRequest };
            GithubAccountListModel result = await Mediator.Send(getListGithubAccountQuery);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubAccountCommand updateGithubAccountCommand)
        {
            UpdatedGithubAccountDto result = await Mediator.Send(updateGithubAccountCommand);
            return Ok(result);
        }




        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubAccountCommand deleteGithubAccountCommand)
        {
            DeletedGithubAccountDto result = await Mediator.Send(deleteGithubAccountCommand);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdGithubAccountQuery getByIdGithubAccountQuery)
        {
            GithubAccountGetByIdDto result = await Mediator.Send(getByIdGithubAccountQuery);
            return Ok(result);
        }





    }
}
