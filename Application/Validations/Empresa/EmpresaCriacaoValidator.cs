using FluentValidation;
using System.Text.RegularExpressions;
using WebMesaGestor.Application.DTO.Input.Empresa;

namespace WebMesaGestor.Application.Validations.Empresa
{
    public class EmpresaCriacaoValidator : AbstractValidator<EmpCriacaoDTO>
    {
        public EmpresaCriacaoValidator()
        {
            RuleFor(x => x.EmpNome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.EmpCnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Must(ValidacaoCNPJ).WithMessage("CNPJ inválido.");
        }

        private bool ValidacaoCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            cnpj = Regex.Replace(cnpj, @"\D", "");

            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}