using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Produto;

namespace WebMesaGestor.Application.Validations.Produto
{
    public class ProdutoEdicaoValidator : AbstractValidator<ProEdicaoDTO>
    {
        public ProdutoEdicaoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do Produto é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("ID do produto inválido.");

            RuleFor(x => x.ProCodigo)
                .NotEmpty().WithMessage("Código do produto é obrigatório.")
                .GreaterThan(0).WithMessage("O código do produto deve ser maior que zero.")
                .LessThanOrEqualTo(999999).WithMessage("O código do produto deve ter no máximo 6 dígitos.");

            RuleFor(x => x.ProDescricao)
               .NotEmpty().WithMessage("Descrição é obrigatória.")
               .MinimumLength(3).WithMessage("A descrição deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.ProUnidade)
                .NotEmpty().WithMessage("Unidade é obrigatória.")
                .Length(2).WithMessage("A unidade deve ter exatamente 2 caracteres (exemplo: 'KG', 'UN').");

            RuleFor(x => x.ProPreco)
                    .NotEmpty().WithMessage("Valor é obrigatório.")
                    .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
                    .LessThanOrEqualTo(10000000).WithMessage("O valor não pode ser maior que 10.000.000")
                    .Must(v => v * 100 % 1 == 0).WithMessage("O valor deve ter no máximo duas casas decimais.");

            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage("CategoriaId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("CategoriaId inválido.");

            RuleFor(x => x.SetorId)
                .NotEmpty().WithMessage("SetorId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("SetorId inválido.");
        }
    }
}
