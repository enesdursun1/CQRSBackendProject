using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        if (user != null) throw new BusinessException("User exists");
    }

    public async Task UserShouldBeExistWhenLogin(string email)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email);
        if (user is null) throw new BusinessException("User not exists");
    }
    public void CheckIfPasswordIsCorrect(string requestPassword, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        if (!HashingHelper.VerifyPasswordHash(requestPassword, userPasswordHash, userPasswordSalt))
            throw new BusinessException("Wrong password");
    }

}
