using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Grupo.ListarGrupo
{
    public class ListarAgendaRequest : ICustomQueryable, IQueryPaging, IQuerySort, IRequest<Response>
    {
        private Guid _idUsuario;

        //Campos customizados
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Sort { get; set; }

        //Filtrar por nome
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string Nome { get; set; }

        public Guid? IdTemplo { get; set; }
        
    }
}
