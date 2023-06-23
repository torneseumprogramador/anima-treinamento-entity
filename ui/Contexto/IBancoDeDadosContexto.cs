using Microsoft.EntityFrameworkCore;
using entity.Entidades;
using System.Text.RegularExpressions;
using System;

namespace entity.Contexto;

public interface IBancoDeDadosContexto
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<Cliente> Clientes { get; set; }
    DbSet<Fornecedor> Fornecedores { get; set; }
    DbSet<Pedido> Pedidos { get; set; }
    DbSet<Produto> Produtos { get; set; }
    DbSet<PedidoProduto> PedidosProdutos { get; set; }
}
