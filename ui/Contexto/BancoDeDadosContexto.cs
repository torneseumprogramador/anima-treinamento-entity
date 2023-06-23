using Microsoft.EntityFrameworkCore;
using entity.Entidades;
using System.Text.RegularExpressions;
using System;

namespace entity.Contexto;

public class BancoDeDadosContexto : DbContext
{
    // estratégia 3, passando a dependencia do banco de dados via construtor
    public BancoDeDadosContexto(DbContextOptions<BancoDeDadosContexto> options) : base(options)
    {
    }

    private string nomeBanco;
    public BancoDeDadosContexto() { }
    public BancoDeDadosContexto(string nomeBanco)
    { 
        this.nomeBanco = nomeBanco;
    }

    // modo 2 de especificar os dados em uma tabela
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region "Clientes"
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("tb_clientes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("cli_id")
                .ValueGeneratedOnAdd()
                .ValueGeneratedOnAdd().UseMySqlIdentityColumn();

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("cli_nome");

            entity.Property(e => e.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("cli_telefone")
                .HasComment("Este é o número de telefone do cliente.");

            entity.Property(e => e.Observacao)
                .HasColumnType("text");
        });
        #endregion

        #region "Produto"
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("produtos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .ValueGeneratedOnAdd().UseMySqlIdentityColumn();

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nome");

            entity.Property(e => e.Valor)
                .IsRequired()
                .HasColumnName("valor");

            entity.Property(e => e.Descricao)
                .HasColumnType("text");
        });
        #endregion

        base.OnModelCreating(modelBuilder);
    }

    // estratégia 1 = vc pode criar a instancia de onde estiver do seu contexto
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("conexao");

            if(!string.IsNullOrEmpty(connectionString))
            {
                string regex = "database=(.*?);";
                connectionString = Regex.Replace(connectionString, regex, $"database={this.nomeBanco};");
            }

            optionsBuilder.UseMySql(connectionString, 
                new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }

    public virtual DbSet<Cliente> Clientes { get; set; } = default!;
    public virtual DbSet<Fornecedor> Fornecedores { get; set; } = default!;
    public virtual DbSet<Pedido> Pedidos { get; set; } = default!;
    public virtual DbSet<Produto> Produtos { get; set; } = default!;
    public virtual DbSet<PedidoProduto> PedidosProdutos { get; set; } = default!;
}
