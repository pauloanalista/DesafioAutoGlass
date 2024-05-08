using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using AutoGlass.Domain.Entities;
using AutoGlass.Infra.Repositories.Map;

namespace AutoGlass.Infra.Repositories.Base
{
    public partial class Context : DbContext
    {
        //Criar as tabelas
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Produto> Produto { get; set; }

        IConfiguration _configuration;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public Context(DbContextOptions<Context> options) : base(options) { }

        //public Context(string conexao)
        //{
        //    _conexao = conexao;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conexao = _configuration.GetSection("Conexao").Value;
                //Habilito para gerar o migration
                //conexao = "Server=162.241.101.197;Database=ilovecod_AutoGlassdev;Uid=ilovecod_AutoGlassdev;Pwd=ilovecod_AutoGlassdev; Connection Timeout=120";
                conexao = "Server=162.241.101.197;Database=ilovecod_AutoGlass;Uid=ilovecod_AutoGlass;Pwd=ilovecod_AutoGlass; Connection Timeout=120";

                //optionsBuilder.UseMySql(_conexao, ServerVersion.AutoDetect(_conexao));
                optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao),
                    b => b
                    .CharSetBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.CharSetBehavior.NeverAppend)// Nao usar utf8mb4
                    );
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ignorar classes
            modelBuilder.Ignore<Notification>();

            //aplicar configurações
            modelBuilder.ApplyConfiguration(new MapFornecedor());
            modelBuilder.ApplyConfiguration(new MapProduto());

            base.OnModelCreating(modelBuilder);
        }


    }
}
