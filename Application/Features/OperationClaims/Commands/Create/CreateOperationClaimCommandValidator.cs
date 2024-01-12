using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimValidator : AbstractValidator<CreateOperationClaimCommand>
{
    public CreateOperationClaimValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

    }
}