using FactoryControll.Domain.Entities.Colaboradores;
using FluentValidation;

namespace FactoryControll.API.Areas.Colaboradores.Validators
{
    public class ColaboradorValidator : AbstractValidator<Colaborador>
    {
        public ColaboradorValidator()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("CPF deve conter exatamente 11 números.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Must(NomeValido).WithMessage("Nome deve conter nome e sobrenome iniciando com letra maiúscula.");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser no passado.");

            RuleFor(c => c.DataAdmissao)
                .NotEmpty().WithMessage("Data de admissão é obrigatória.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Data de admissão não pode ser futura.");

            RuleFor(c => c.DataRescisao)
                .GreaterThanOrEqualTo(c => c.DataAdmissao)
                .When(c => c.DataRescisao.HasValue)
                .WithMessage("Data de rescisão não pode ser anterior à data de admissão.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(c => c.CargoId)
                .NotEmpty().WithMessage("Cargo é obrigatório.");

            RuleFor(c => c.FuncaoId)
                .NotEmpty().WithMessage("Função é obrigatória.");
        }

        private bool NomeValido(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return false;

            var partes = nome.Trim().Split(' ');
            if (partes.Length < 2)
                return false;

            return partes.All(p => char.IsUpper(p[0]));
        }
    }
}
