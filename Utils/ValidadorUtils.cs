using System.Globalization;
using System.Text.RegularExpressions;

namespace WebMesaGestor.Utils
{
    public class ValidadorUtils
    {
        public static void ValidarSeIgual(object objeto1, object objeto2, string mensagem)
        {
            if (!objeto1.Equals(objeto2))
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeDiferente(object objeto1, object objeto2, string mensagem)
        {
            if (objeto1.Equals(objeto2))
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMaximo(string valor, int maximo, string mensagem)
        {
            var length = valor?.Trim()?.Length ?? 0;

            if (length > maximo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMaximo(decimal valor, int maximo, string mensagem)
        {
            if (valor > maximo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMinimo(string valor, int minimo, string mensagem)
        {
            var length = valor?.Trim()?.Length ?? 0;

            if (length < minimo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMinimo(decimal valor, decimal minimo, string mensagem)
        {
            if (valor < minimo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarTamanhoRequerido(string valor, int tamanhoRequerido, string mensagem)
        {
            var length = valor?.Trim()?.Length ?? 0;

            if (length != tamanhoRequerido)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeVazioOuNulo(string valor, string mensagem)
        {
            if (string.IsNullOrEmpty(valor))
            {
                throw new Exception(mensagem);
            }
        }
        public static void ValidarSeEhNulo(object objeto, string mensagem)
        {
            if (objeto == null)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarNaoEhSeNulo(object objeto, string mensagem)
        {
            if (objeto != null)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMinimoMaximo(string valor, int minimo, int maximo, string mensagem)
        {
            var length = valor?.Trim()?.Length ?? 0;

            if (length < minimo || length > maximo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMinimoMaximo(int valor, decimal minimo, int maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeMenorIgualMinimo(decimal valor, decimal minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeMenorIgualMinimo(int valor, int minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeFalso(bool boolValor, string mensagem)
        {
            if (!boolValor)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarSeVerdadeiro(bool boolValor, string mensagem)
        {
            if (boolValor)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarNegativo(decimal valor, string mensagem)
        {
            if (valor < 0)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarMenorIgualZero(decimal? valor, string mensagem)
        {
            if ((valor ?? 0) <= 0)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarDecimal(decimal valor, string mensagem)
        {
            string valorString = valor.ToString("F2", CultureInfo.InvariantCulture);
            string[] partes = valorString.Split('.');
            string parteInteira = partes[0];
            string parteDecimal = partes.Length > 1 ? partes[1] : "00";

            if (parteInteira.Length > 16 || parteDecimal.Length > 2)
            {
                throw new Exception(mensagem);
            }
        }

        public static void ValidarCnpj(string cnpj, string mensagem)
        {
            if (!ValidadorCNPJ(cnpj))
            {
                throw new Exception(mensagem);
            }
        }

        private static bool ValidadorCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                return false;
            }

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            int[] multiplicador1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            int resto = (soma % 11);
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        //Validações com Regex
        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }

        public bool ValidarSenha(string senha)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(senha);
        }

        public bool ValidarNumeroTel(string numero)
        {
            Regex regex = new Regex(@"^\d{10,15}$");
            return regex.IsMatch(numero);
        }
    }
}
