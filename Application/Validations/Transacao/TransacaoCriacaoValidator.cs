using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Transacao;

namespace WebMesaGestor.Application.Validations.Transacao
{
    public class TransacaoCriacaoValidator : AbstractValidator<TraCriacaoDTO>
    {
        public TransacaoCriacaoValidator()
        {
            RuleFor(x => x.TraDescricao)
                    .NotEmpty().WithMessage("Descrição é obrigatória.");

            RuleFor(x => x.TraValor)
                    .NotEmpty().WithMessage("Valor é obrigatório.")
                    .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
                    .LessThanOrEqualTo(10000000).WithMessage("O valor não pode ser maior que 10.000.000")
                    .Must(v => v * 100 % 1 == 0).WithMessage("O valor deve ter no máximo duas casas decimais.");

            RuleFor(x => x.TransacaoStatus)
                    .NotEmpty().WithMessage("Status é obrigatório.")
                    .IsInEnum().WithMessage("Status inválido.");

            RuleFor(x => x.UsuarioId)
                    .NotEmpty().WithMessage("EmpresaId é obrigatório.");

            RuleFor(x => x.CaixaId)
                    .NotEmpty().WithMessage("CaixaId é obrigatório.");

            RuleFor(x => x.PedidoId)
                .NotEmpty().WithMessage("PedidoId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("PedidoId inválido.");
        }
    }
}
