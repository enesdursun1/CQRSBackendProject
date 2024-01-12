﻿using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.AuthServices;

public interface IAuthService
{
    public Task<AccessToken> CreateAccessToken(User user);
    public Task<RefreshToken> CreateRefreshToken(User user, string idAddress);
    public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);

}