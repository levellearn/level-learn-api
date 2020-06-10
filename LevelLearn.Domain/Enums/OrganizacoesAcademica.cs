using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum OrganizacoesAcademica
    {
        [Description("Centro Universitário")]
        CentroUniversitario,
        [Description("Faculdade")]
        Faculdade,
        [Description("Universidade")]
        Universidade,
        [Description("Instituto Federal de Educação Ciência e Tecnologia")]
        InstitutoFederalDeEducacaoCienciaETecnologia,
        [Description("Outros")]
        Outros

    }
}
