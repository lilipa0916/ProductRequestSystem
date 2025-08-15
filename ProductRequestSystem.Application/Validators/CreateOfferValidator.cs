using FluentValidation;
using ProductRequestSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRequestSystem.Application.Validators
{
    public class CreateOfferValidator : AbstractValidator<CreateOfferDto>
    {
        public CreateOfferValidator()
        {
            RuleFor(x => x.ProductRequestId)
                .GreaterThan(0).WithMessage("ID de solicitud inválido");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(x => x.EstimatedDays)
                .GreaterThan(0).WithMessage("Los días estimados deben ser mayor a 0");

            RuleFor(x => x.Comments)
                .MaximumLength(500).WithMessage("Los comentarios no pueden exceder 500 caracteres");
        }
    }
}
