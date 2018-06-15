using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LevelLearn.Web.Extensions.Common
{
    public static class ModelStateExtensions
    {
        public static string DisplayErros(this ModelStateDictionary modelState)
        {
            var erros = "";

            foreach (var values in modelState.Values)
            {
                foreach (var item in values.Errors)
                {
                    erros = erros + item.ErrorMessage + "<br />";
                }
            }

            return erros;
        }
    }
}
