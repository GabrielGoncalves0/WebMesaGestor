using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Caixa;

namespace WebMesaGestor.Application.Validations.Caixa
{
    public class CaiFecharValidator : AbstractValidator<CaiFecharDTO>
    {
        public CaiFecharValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do caixa é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do caixa inválido.");

            RuleFor(x => x.CaiValFechamento)
                .NotEmpty().WithMessage("O valor de fechamento é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O valor de fechamento não pode ser negativo.")
                .Must(v => v * 100 % 1 == 0).WithMessage("O valor de fechamento deve ter no máximo duas casas decimais.");
        }
    }
}
