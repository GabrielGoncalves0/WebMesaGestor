using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Grupo;

namespace WebMesaGestor.Application.Validations.Grupo
{
    public class GrupOpcEdicaoValidator : AbstractValidator<GrupOpcEdicaoDTO>
    {
        public GrupOpcEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do grupo de opções é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do grupo de opções inválido.");

            RuleFor(x => x.GrupOpcDesc)
                .NotEmpty().WithMessage("A descrição do grupo de opções é obrigatória.")
                .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("A descrição pode ter no máximo 100 caracteres.");

            RuleFor(x => x.GrupOpcTipo)
                .IsInEnum().WithMessage("Tipo do grupo de opções inválido.");

            RuleFor(x => x.GrupOpcMax)
                .GreaterThan(0).WithMessage("O valor máximo de opções deve ser maior que zero.");

            RuleFor(x => x.ProdutoId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do produto inválido.");
        }
    }
}
