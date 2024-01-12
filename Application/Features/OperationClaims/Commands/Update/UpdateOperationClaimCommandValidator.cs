using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.Update; 

public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateOperationClaimCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);


    }
}
