namespace Estoque.Crosscutting.Extensions
{
    public static class StringExtensions
    {
        public static decimal ConvertToDecimal(this string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                Console.WriteLine($"Conversão para decimal bem-sucedida: {result}");
                return result;
            }

            Console.WriteLine("Falha na conversão para decimal");
            return 0;
        }
    }
}
