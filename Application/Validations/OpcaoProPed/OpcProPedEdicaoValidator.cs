using FluentValidation;
using WebMesaGestor.Application.DTO.Input.OpcaoProPed;

namespace WebMesaGestor.Application.Validations.OpcaoProPed
{
    public class OpcProPedEdicaoValidator : AbstractValidator<OpcProPedEdicaoDTO>
    {
        public OpcProPedEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da opção de produto pedido é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID da opção de produto pedido inválido.");

            RuleFor(x => x.ProdutoPedidoId)
                .NotEmpty().WithMessage("O ID do produto pedido é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do produto pedido inválido.");

            RuleFor(x => x.OpcaoId)
                .NotEmpty().WithMessage("O ID da opção é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID da opção inválido.");
        }
    }
}
