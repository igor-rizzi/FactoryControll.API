using FactoryControll.API.Areas.Administracao.Models;
using FluentValidation;

namespace FactoryControll.API.Areas.Administracao.Validators
{
    public class FuncaoValidator : AbstractValidator<FuncaoModel>
    {
        public FuncaoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da função é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da função deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da função é obrigatória.")
                .MaximumLength(255).WithMessage("A descrição da função deve ter no máximo 255 caracteres.");
        }
    }
}