using System.Text;

namespace Core;

public static class PhonewordTranslator
{
    // clase estatica que recibe una variable string
    public static string ToNumber(string raw)
    {
        // Se fija que el campo no este en blanco
        if (string.IsNullOrWhiteSpace(raw))
            return null;
        // El string que recibe lo convierte en mayusculas
        raw = raw.ToUpperInvariant();

        // A partir de aqui hace la conversión
        var newNumber = new StringBuilder();
        foreach (var c in raw)
        {
            // Si es un numero lo mantiene
            if (" -0123456789".Contains(c))
                newNumber.Append(c);

            // Si se puso un caracter especial como un * o una coma o un punto devuelve un nulo
            else
            {
                var result = TranslateToNumber(c);
                if (result != null)
                    newNumber.Append(result);
                // Bad character?
                else
                    return null;
            }
        }
        return newNumber.ToString();
    }

    static bool Contains(this string keyString, char c)
    {
        return keyString.IndexOf(c) >= 0;
    }

    // Traduccion de las letras como en los telefonos antiguos. 
    static readonly string[] digits = {
        "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    // Si recibe una letra lo convertira a numero.
    static int? TranslateToNumber(char c)
    {
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i].Contains(c))
                return 2 + i;
        }
        return null;
    }
}
