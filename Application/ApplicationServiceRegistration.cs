using Application.Features.ProgrammingLanguages.Rules;
using Application.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Application.Features.ProgrammingLanguages.Commands.Create;
using Domain.Entities;
using FluentValidation;
using Core.Application.Pipelines.Validation;
using Application.Features.Technologies.Rules;
using Application.Features.Auths.Rules;
using Application.Services.AuthServices;
using Core.Security.JWT;
using Application.Features.OperationClaims.Rules;
using Application.Features.UserOperationClaims.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Application.Features.GithubAccounts.Rules;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));


        });

        services.AddScoped<ProgrammingLanguageBusinessRules>();

        services.AddScoped<TechnologyBusinessRules>();

        services.AddScoped<AuthBusinessRules>();

        services.AddScoped<OperationClaimBusinessRules>();

        services.AddScoped<UserOperationClaimBusinessRules>();

        services.AddScoped<GithubAccountBusinessRules>();
      

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
      




        services.AddScoped<IAuthService, AuthManager>();

        services.AddScoped<ITokenHelper, JwtHelper>();





        return services;




    }


}
