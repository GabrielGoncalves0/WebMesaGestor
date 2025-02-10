using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Pedido;

namespace WebMesaGestor.Application.Validations.Pedido
{
    public class PedEdicaoValidator : AbstractValidator<PedEdicaoDTO>
    {
        public PedEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do pedido é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do pedido inválido.");

            RuleFor(x => x.PedValor)
                    .NotEmpty().WithMessage("Valor é obrigatório.")
                    .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
                    .LessThanOrEqualTo(10000000).WithMessage("O valor não pode ser maior que 10.000.000")
                    .Must(v => v * 100 % 1 == 0).WithMessage("O valor deve ter no máximo duas casas decimais.");

            RuleFor(x => x.PedStatus)
                .IsInEnum().WithMessage("Status do pedido inválido.");

            RuleFor(x => x.PedTipoPag)
                .IsInEnum().WithMessage("Tipo de pagamento inválido.");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("Usuário é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("Usuário inválido.");

            RuleFor(x => x.MesaId)
                .NotEmpty().WithMessage("Mesa é obrigatória.")
                .NotEqual(Guid.Empty).WithMessage("Mesa inválida.");
        }
    }
}
