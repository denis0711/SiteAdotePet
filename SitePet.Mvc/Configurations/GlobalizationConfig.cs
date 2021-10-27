using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace SitePet.Mvc.Configurations
{
    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {

            var defaulCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaulCulture),
                SupportedCultures = new List<CultureInfo> { defaulCulture },
                SupportedUICultures = new List<CultureInfo> { defaulCulture }
            };
            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
