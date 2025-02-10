using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Caixa;

namespace WebMesaGestor.Application.Validations.Caixa
{
    public class CaiAtualizarValidator : AbstractValidator<CaiAtualizarDTO>
    {
        public CaiAtualizarValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do caixa é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do caixa inválido.");

            RuleFor(x => x.CaiValTotal)
                .GreaterThanOrEqualTo(0).WithMessage("O valor total do caixa não pode ser negativo.")
                .Must(v => v == null || v * 100 % 1 == 0).WithMessage("O valor total deve ter no máximo duas casas decimais.");

            RuleFor(x => x.CaiStatus)
                .IsInEnum().WithMessage("Status do caixa inválido.");
        }
    }
}
