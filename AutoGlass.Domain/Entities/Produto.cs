using AutoGlass.Domain.Entities.Base;
using AutoGlass.Domain.Enums.Produto;
using AutoGlass.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;

namespace AutoGlass.Domain.Entities
{
    public class Produto : EntityBase
    {
        public Produto(Fornecedor fornecedor, string codigo, string descricao, DateTime dataFabricacao, DateTime dataValidade)
        {
            Codigo = codigo;
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            Fornecedor = fornecedor;
            Situacao = EnumSituacao.Ativo;

            new AddNotifications<Produto>(this)
                .IfRequired(x => x.Codigo, 1, 50, "O codigo deve ter entre 2 a 50 caracteres")
                .IfRequired(x => x.Descricao, 1, 1000)
                .IfNull(x => x.DataFabricacao, MSG.X0_INVALIDA.ToFormat("Data Fabricação"))
                .IfNull(x => x.DataValidade, MSG.X0_INVALIDA.ToFormat("Data Validade"))
                .IfEnumInvalid(x => x.Situacao, "Situação inválida")
                .IfNull(x => x.Fornecedor, MSG.X0_E_OBRIGATORIO.ToFormat("Fornecedor"))
                //.IfGreaterOrEqualsThan(x=>x.DataFabricacao, dataValidade,"Data de fabricação não pode ser maior que a data da validade")
                ;

            //Outra forma de validar a informação
            if (DataFabricacao >= DataValidade)
            {
                AddNotification("DataFabricacao", "Data de fabricação não pode ser maior que a data da validade");
            }
        }
        public void AlterarProduto(Fornecedor fornecedor, string codigo, string descricao, DateTime dataFabricacao, DateTime dataValidade)
        {
            Codigo = codigo;
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            Fornecedor = fornecedor;

            new AddNotifications<Produto>(this)
                .IfRequired(x => x.Codigo, 1, 50)
                .IfRequired(x => x.Descricao, 1, 1000)
                .IfNull(x => x.DataFabricacao, MSG.X0_INVALIDA.ToFormat("Data Fabricação"))
                .IfNull(x => x.DataValidade, MSG.X0_INVALIDA.ToFormat("Data Validade"))
                .IfEnumInvalid(x => x.Situacao, "Situação inválida")
                .IfNull(x => x.Fornecedor, MSG.X0_E_OBRIGATORIO.ToFormat("Fornecedor"));

            //Outra forma de validar a informação
            if (DataFabricacao >= DataValidade)
            {
                AddNotification("DataFabricacao", "Data de fabricação não pode ser maior que a data da validade");
            }
        }

        public void InativarProduto()
        {
            Situacao = EnumSituacao.Inativo;
        }
        protected Produto()
        {

        }
        public string Codigo { get; set; }

        public string Descricao { get; private set; }
        public EnumSituacao Situacao { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public Fornecedor Fornecedor { get; set; }

        
    }
}
