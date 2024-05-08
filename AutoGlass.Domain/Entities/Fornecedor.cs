using AutoGlass.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        protected Fornecedor()
        {

        }
        public string Codigo { get; private set; }
        public string Descricao { get; set; }
        public string CNPJ { get; private set; }
    }
}
