using System.ComponentModel;

namespace API_Nox.Enums
{
    public enum StatusTask
    {
        [Description("A Fazer")]
        AFazer = 1,
        [Description("Em Andamento")]
        EmAndamento = 2,
        [Description("Concluído")]
        Concluído = 3
    }
}
