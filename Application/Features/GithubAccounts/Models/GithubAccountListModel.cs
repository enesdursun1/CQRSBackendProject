using Application.Features.GithubAccounts.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubAccounts.Models;

public class GithubAccountListModel:BasePageableModel
{
    public IList<GithubAccountListDto> Items { get; set; }

}
