using LevelLearn.Domain.Enum;
using LevelLearn.ViewModel.Enum;
using System;
using System.Collections.Generic;

namespace LevelLearn.Web.Extensions.Common
{
    public static class StatusResponseEnumExtensions
    {
        public static string DisplayDescriptionsToViewModel(this List<StatusResponseEnum> allStatus)
        {
            string erros = string.Empty;
            try
            {
                foreach (var item in allStatus)
                {
                    StatusResponseEnumViewModel status = (StatusResponseEnumViewModel)item;
                    erros = erros + $"{status.DisplayDescription()} \n";
                }
            }
            catch (Exception ex)
            {
                erros = "Erro ao criar mensagens de erro";
            }
            return erros;
        }

        public static string DisplayDescriptionToViewModel(this StatusResponseEnum st)
        {
            string erro = string.Empty;
            try
            {
                StatusResponseEnumViewModel status = (StatusResponseEnumViewModel)st;
                erro = $"{status.DisplayDescription()}";
            }
            catch (Exception ex)
            {
                erro = "Erro ao criar mensagem de erro";
            }
            return erro;
        }
    }
}
