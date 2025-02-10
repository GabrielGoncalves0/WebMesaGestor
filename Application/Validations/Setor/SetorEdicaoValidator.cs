using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Setor;

namespace WebMesaGestor.Application.Validations.Setor
{
    public class SetorEdicaoValidator : AbstractValidator<SetEdicaoDTO>
    {
        public SetorEdicaoValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("O ID do setor é obrigatório.")
                    .NotEqual(Guid.Empty).WithMessage("ID do setor inválido.");

            RuleFor(x => x.SetDesc)
                    .NotEmpty().WithMessage("Descrição é obrigatória.");

            RuleFor(x => x.SetStatus)
                    .NotEmpty().WithMessage("Status é obrigatório.")
                    .IsInEnum().WithMessage("Status inválido.");
        }
    }
}
