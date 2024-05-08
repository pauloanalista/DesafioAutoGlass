using System.ComponentModel;

namespace AutoGlass.Domain.Enums.Produto
{
    public enum EnumSituacao
    {
        [Description("Produto ativo")]
        Ativo,
        
        [Description("Produto inativo")]
        Inativo
    }
}

