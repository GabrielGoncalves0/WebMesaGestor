using FluentValidation;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;

namespace WebMesaGestor.Application.Validations.ProdutoPedido
{
    public class ProPedCriacaoValidator : AbstractValidator<ProPedCriacaoDTO>
    {
        public ProPedCriacaoValidator()
        {
            RuleFor(x => x.ProPedQuant)
                .GreaterThan(0).WithMessage("A quantidade do produto no pedido deve ser maior que zero.");

            RuleFor(x => x.ProPedDesconto)
                .InclusiveBetween(0, 100).WithMessage("O desconto deve estar entre 0 e 100.");

            RuleFor(x => x.ProdutoId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do produto inválido.");

            RuleFor(x => x.PedidoId)
                .NotEmpty().WithMessage("O ID do pedido é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do pedido inválido.");
        }
    }
}
