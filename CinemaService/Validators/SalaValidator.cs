using CinemaDomain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaService.Validators
{
    public class SalaValidator : AbstractValidator<Sala>
    {
        public SalaValidator()
        {
            RuleFor(s => s.Numero).GreaterThanOrEqualTo(0).WithMessage("Número da sala deve ser maior que 0!");
            RuleFor(s => s.Capacidade).GreaterThanOrEqualTo(0).WithMessage("Capacidade da sala deve ser maior que 0!");
            RuleFor(s => s.Fileiras).GreaterThanOrEqualTo(0).WithMessage("Fileiras devem ser maior que 0!");
            RuleFor(s => s.Assentos).GreaterThanOrEqualTo(0).WithMessage("Assentos devem ser maior que 0!");

            RuleFor(s => s.Capacidade).Equal(s => s.Fileiras * s.Assentos).When(s => s.Fileiras > 0 && s.Assentos > 0).WithMessage("Capacidade deve ser igual a Fileiras * Assentos");
        }
    }
}
