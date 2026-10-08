using CinemaDomain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaService.Validators
{
    public class GeneroValidator : AbstractValidator<Genero>
    {
        public GeneroValidator()
        {
            RuleFor(g => g.Nome).NotEmpty().WithMessage("Genero deve ser informado").NotNull().WithMessage("Genereo deve ser informado")
                .Length(5, 50).WithMessage("Genero deve conter entre 5 e 50 caracteres");
        }
    }
}
