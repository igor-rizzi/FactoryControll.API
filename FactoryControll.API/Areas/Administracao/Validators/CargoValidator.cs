using FactoryControll.API.Areas.Administracao.Models;
using FluentValidation;

namespace FactoryControll.API.Areas.Administracao.Validators
{
    public class CargoValidator : AbstractValidator<CargoModel>
    {
        public CargoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do cargo é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do cargo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição do cargo é obrigatória.")
                .MaximumLength(255).WithMessage("A descrição do cargo deve ter no máximo 255 caracteres.");
        }
    }
}