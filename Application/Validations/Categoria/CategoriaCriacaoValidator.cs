using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Categoria;

namespace WebMesaGestor.Application.Validations.Categoria
{
    public class CatCriacaoValidator : AbstractValidator<CatCriacaoDTO>
    {
        public CatCriacaoValidator()
        {
            RuleFor(x => x.CatDesc)
                .NotEmpty().WithMessage("A descrição da categoria é obrigatória.")
                .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("A descrição pode ter no máximo 100 caracteres.");
        }
    }
}

