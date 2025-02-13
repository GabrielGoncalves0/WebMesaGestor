using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Opcoes;

namespace WebMesaGestor.Application.Validations.Opcoes
{
    public class OpcaoCriacaoValidator : AbstractValidator<OpcCriacaoDTO>
    {
        public OpcaoCriacaoValidator()
        {
            RuleFor(x => x.OpcaoDesc)
                .NotEmpty().WithMessage("A descrição da opção é obrigatória.")
                .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.OpcaoValor)
                .NotEmpty().WithMessage("O valor da opção é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O valor da opção não pode ser negativo.")
                .LessThanOrEqualTo(10000000).WithMessage("O valor não pode ser maior que 10.000.000")
                .Must(v => v * 100 % 1 == 0).WithMessage("O valor deve ter no máximo duas casas decimais.");

            RuleFor(x => x.OpcaoQuantMax)
                .NotEmpty().WithMessage("A quantidade máxima é obrigatória.")
                .GreaterThan(0).WithMessage("A quantidade máxima deve ser maior que zero.");

            RuleFor(x => x.GrupoOpcoesId)
                .NotEmpty().WithMessage("GrupoOpcoesId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("GrupoOpcoesId inválido.");
        }
    }
}
