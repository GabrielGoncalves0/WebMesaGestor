using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Caixa;

namespace WebMesaGestor.Application.Validations.Caixa
{
    public class CaiAbrirValidator : AbstractValidator<CaiAbrirDTO>
    {
        public CaiAbrirValidator()
        {
            RuleFor(x => x.CaiValInicial)
                .NotEmpty().WithMessage("O valor inicial é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O valor inicial não pode ser negativo.")
                .Must(v => v * 100 % 1 == 0).WithMessage("O valor inicial deve ter no máximo duas casas decimais.");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do usuário inválido.");
        }
    }
}
