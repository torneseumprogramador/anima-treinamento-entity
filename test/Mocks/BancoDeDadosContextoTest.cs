// using entity.Contexto;
// using entity.Entidades;
// using entity.Patterns;
// using Microsoft.EntityFrameworkCore;

// namespace test.Mocks;

// public class BancoDeDadosContextoTest : IBancoDeDadosContexto
// {
//     public DbSet<Cliente> Clientes { get; set; } = default!;
//     public DbSet<Fornecedor> Fornecedores { get; set; } = default!;
//     public DbSet<Pedido> Pedidos { get; set; } = default!;
//     public DbSet<Produto> Produtos { get; set; } = default!;
//     public DbSet<PedidoProduto> PedidosProdutos { get; set; } = default!;

//     public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//     {
//        await Task.FromResult(1);
//     }
// }