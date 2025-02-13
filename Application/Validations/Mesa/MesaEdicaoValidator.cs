using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Mesa;

namespace WebMesaGestor.Application.Validations.Mesa
{
    public class MesaEdicaoValidator : AbstractValidator<MesEdicaoDTO>
    {
        public MesaEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da mesa é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID da mesa inválido.");

            RuleFor(x => x.MesaNumero)
                .NotEmpty().WithMessage("O número da mesa é obrigatório.")
                .GreaterThan(0).WithMessage("O número da mesa deve ser maior que zero.");

            RuleFor(x => x.MesaStatus)
                .IsInEnum().WithMessage("Status da mesa inválido.");
        }
    }
}
