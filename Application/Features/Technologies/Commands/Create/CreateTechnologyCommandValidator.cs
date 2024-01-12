using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.Create;

public class CreateTechnologyCommandValidator :AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);

    }
}
