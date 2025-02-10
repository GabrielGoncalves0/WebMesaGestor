using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Setor;

namespace WebMesaGestor.Application.Validations.Setor
{
    public class SetorCriacaoValidator : AbstractValidator<SetCriacaoDTO>
    {
        public SetorCriacaoValidator()
        {
            RuleFor(x => x.SetDesc)
                .NotEmpty().WithMessage("Descrição é obrigatória.");

            RuleFor(x => x.SetStatus)
                .NotEmpty().WithMessage("Status é obrigatório.")
                .IsInEnum().WithMessage("Status inválido.");
        }
    }
}
