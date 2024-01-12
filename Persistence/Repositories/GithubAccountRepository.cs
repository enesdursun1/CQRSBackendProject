using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GithubAccountRepository : EfRepositoryBase<GithubAccount, BaseDbContext>, IGithubAccountRepository
{
    public GithubAccountRepository(BaseDbContext context) : base(context)
    {
    }
}
