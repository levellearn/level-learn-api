﻿using System.ComponentModel;

namespace LevelLearn.Domain.Enums
{
    public enum GeneroPessoa
    {
        [Description("Vazio")]
        Vazio = 0,
        [Description("Masculino")]
        Masculino,
        [Description("Feminino")]
        Feminino,
        [Description("Outros")]
        Outros
    }
}
