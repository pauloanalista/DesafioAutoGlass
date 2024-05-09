using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using AutoGlass.Domain.Entities;
using AutoGlass.Infra.Repositories.Map;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace AutoGlass.Infra.Repositories.Base
{
    public partial class Context : DbContext
    {
        //Criar as tabelas
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Produto> Produto { get; set; }

        //IConfiguration _configuration;

        //public Context(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            if (!optionsBuilder.IsConfigured)
            {
                string connection = "Server=127.0.0.1;Database=autoglass;Uid=root;Pwd=root; Connection Timeout=120";


                optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
            }


            //if (!optionsBuilder.IsConfigured)
            //{

            //    //Habilito para gerar o migration
            //    string conexao = "Server=127.0.0.1;Database=autoglass;Uid=root;Pwd=root; Connection Timeout=120";
            //    //optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
            //    optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao),
            //        b => b
            //        .CharSetBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.CharSetBehavior.NeverAppend)// Nao usar utf8mb4
            //        );
            //}
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

    //public class MyAppDbContextFactory : IDesignTimeDbContextFactory<Context>
    //{
    //    public Context CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<Context>();

    //        // Configure a string de conexão
    //        string connection = "Server=127.0.0.1;Database=autoglass;Uid=root;Pwd=root; Connection Timeout=120";
    //        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));


    //        return new Context(optionsBuilder.Options);
    //    }
    //}
}
