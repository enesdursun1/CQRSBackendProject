using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.Update;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

    }
}
