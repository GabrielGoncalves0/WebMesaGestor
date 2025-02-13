using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Categoria;

namespace WebMesaGestor.Application.Validations.Categoria
{
    public class CategoriaEdicaoValidator : AbstractValidator<CatEdicaoDTO>
    {
        public CategoriaEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da categoria é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID da categoria inválido.");

            RuleFor(x => x.CatDesc)
                .NotEmpty().WithMessage("A descrição da categoria é obrigatória.")
                .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("A descrição pode ter no máximo 100 caracteres.");
        }
    }
}
