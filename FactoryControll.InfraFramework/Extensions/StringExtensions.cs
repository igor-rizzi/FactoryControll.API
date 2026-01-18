using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FactoryControll.InfraFramework.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char caractere in textoNormalizado)
            {
                var categoriaUnicode = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (categoriaUnicode != UnicodeCategory.NonSpacingMark)
                    sb.Append(caractere);
            }

            string semAcento = sb.ToString().Normalize(NormalizationForm.FormC);

            string resultado = Regex.Replace(semAcento, @"[^a-zA-Z0-9]", string.Empty);

            return resultado;
        }
    }
}
