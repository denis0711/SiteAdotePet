using Microsoft.AspNetCore.Mvc.Razor;

namespace SitePet.Mvc.Extensions
{
    public static class RazorHelper
    {
        public static string MostrarTipo(this RazorPage page, int tipo)
        {
            return tipo == 1 ? $"Gato" : "Cachorro";
        }

        public static string MostrarGenero(this RazorPage page, int genero)
        {
            return genero == 1 ? "Fêmea" : "Macho";
        }
    }
}
