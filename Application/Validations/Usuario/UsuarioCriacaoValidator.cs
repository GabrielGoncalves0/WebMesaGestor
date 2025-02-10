﻿using FluentValidation;
using WebMesaGestor.Application.DTO.Input.Usuario;

namespace WebMesaGestor.Application.Validations.Usuario
{
    public class UsuarioCriacaoValidator : AbstractValidator<UsuCriacaoDTO>
    {
        public UsuarioCriacaoValidator()
        {
            RuleFor(x => x.UsuNome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.UsuEmail)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.UsuTelefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.")
                .Matches(@"^\d{10,16}$").WithMessage("Telefone deve ser válido.");

            RuleFor(x => x.UsuSenha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,30}$")
                .WithMessage("Senha deve conter pelo menos 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");

            RuleFor(x => x.UsuTipo)
                .NotEmpty().WithMessage("Tipo de usuário é obrigatório.")
                .IsInEnum().WithMessage("Tipo de usuário inválido.");

            RuleFor(x => x.EmpresaId)
                .NotEmpty().WithMessage("EmpresaId é obrigatório.")
                .NotEqual(Guid.Empty).WithMessage("EmpresaId inválido.");
        }
    }
}
