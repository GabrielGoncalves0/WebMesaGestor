using FluentValidation;
using WebMesaGestor.Application.DTO.Input.ProdutoPedido;

namespace WebMesaGestor.Application.Validations.ProdutoPedido
{
    public class ProPedEdicaoValidator : AbstractValidator<ProPedEdicaoDTO>
    {
        public ProPedEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do produto pedido é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do produto pedido inválido.");

            RuleFor(x => x.StatusProPed)
                .IsInEnum().WithMessage("Status do produto no pedido inválido.");
        }
    }
}
