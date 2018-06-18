using System.ComponentModel;

namespace LevelLearn.ViewModel.Enum
{
    public enum StatusResponseEnumViewModel
    {
        [Description("Sucesso")]
        Sucesso = 1,

        [Description("Erro interno do servidor, favor contatar o administrador")]
        ErroInterno = 2,

        [Description("Você já tem um item com esse nome cadastrado")]
        NomeExistente = 3
    }
}
