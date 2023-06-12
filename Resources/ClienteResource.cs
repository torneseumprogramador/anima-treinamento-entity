using entity.Contexto;
using entity.Entidades;
using entity.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ClienteResource
{
    public static RegistroPaginado<PedidoClienteSomadas> ClienteComPedido([FromServices] BancoDeDadosContexto contexto, [FromQuery] int? page)
    {
        var totalPage = 2;
        if(page == null || page < 1) page = 1;

        int offset = ((int)page - 1) * totalPage;

        /*var relatorioEntity = contexto.Pedidos
                                .Include(p => p.Cliente)
                                .Select( p => new PedidoCliente { 
                                    Nome = p.Cliente.Nome,
                                    Telefone = p.Cliente.Telefone,
                                    ValorTotal = p.ValorTotal
        });*/

        /*
        var relatorioEntity = contexto.Pedidos
            .Join(
                contexto.PedidosProdutos,
                pedido => pedido.Id,
                pedidoProduto => pedidoProduto.PedidoId,
                (pedido, pedidoProduto) => new { 
                    Pedido = pedido, 
                    PedidoProduto = pedidoProduto 
                }
            )
            .Join(
                contexto.Produtos,
                p => p.PedidoProduto.ProdutoId,
                produto => produto.Id,
                (p, produto) => new PedidoCliente
                {
                    Id = p.Pedido.Id,
                    Nome = p.Pedido.Cliente.Nome,
                    Telefone = p.Pedido.Cliente.Telefone,
                    ValorTotal = p.Pedido.ValorTotal,
                    NomeProduto = produto.Nome,
                    QuantidadeVendidaParaProduto = p.PedidoProduto.Quantidade,
                    ValorVendidaParaProduto = p.PedidoProduto.Valor
                }
        );
        */

        /*
        // ### Objetos encadeados ###
        var relatorioEntity = contexto.Pedidos
        .Join(
            contexto.PedidosProdutos,
            pedido => pedido.Id,
            pedidoProduto => pedidoProduto.PedidoId,
            (pedido, pedidoProduto) => new { Pedido = pedido, PedidoProduto = pedidoProduto }
        )
        .Join(
            contexto.Produtos,
            p => p.PedidoProduto.ProdutoId,
            produto => produto.Id,
            (p, produto) => new PedidoCliente
            {
                Id = p.Pedido.Id,
                Nome = p.Pedido.Cliente.Nome,
                Telefone = p.Pedido.Cliente.Telefone,
                ValorTotal = p.Pedido.ValorTotal,
                NomeProduto = produto.Nome,
                QuantidadeVendidaParaProduto = p.PedidoProduto.Quantidade,
                ValorVendidaParaProduto = p.PedidoProduto.Valor
            }
        )
        .GroupBy(p => new { p.Id, p.Nome, p.Telefone, p.ValorTotal })
        .Select(g => new PedidoClienteSomadas
        {
            Id = g.Key.Id,
            Nome = g.Key.Nome,
            Telefone = g.Key.Telefone,
            ValorTotal = g.Key.ValorTotal,
            QuantidadeSomadaProduto = g.Sum(p => p.QuantidadeVendidaParaProduto),
            ValorSomadoProduto = g.Sum(p => p.ValorVendidaParaProduto)
        });

        */

        
        // ### link to sql ###
        var relatorioEntity = (from pedido in contexto.Pedidos
            join pedidoProduto in contexto.PedidosProdutos on pedido.Id equals pedidoProduto.PedidoId
            join produto in contexto.Produtos on pedidoProduto.ProdutoId equals produto.Id
        select new PedidoCliente
        {
            Id = pedido.Id,
            Nome = pedido.Cliente.Nome,
            Telefone = pedido.Cliente.Telefone,
            ValorTotal = pedido.ValorTotal,
            NomeProduto = produto.Nome,
            QuantidadeVendidaParaProduto = pedidoProduto.Quantidade,
            ValorVendidaParaProduto = pedidoProduto.Valor
        })
        .GroupBy(p => new { p.Id, p.Nome, p.Telefone, p.ValorTotal })
        .Select(g => new PedidoClienteSomadas
        {
            Id = g.Key.Id,
            Nome = g.Key.Nome,
            Telefone = g.Key.Telefone,
            ValorTotal = g.Key.ValorTotal,
            QuantidadeSomadaProduto = g.Sum(p => p.QuantidadeVendidaParaProduto),
            ValorSomadoProduto = g.Sum(p => p.ValorVendidaParaProduto)
        });

        var lista = relatorioEntity.Skip(offset).Take(totalPage).ToList();

        

        /* // ### query bruta ###
        var connection = contexto.Database.GetDbConnection();
        try
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var query = @"
                    SELECT 
                        p.Id AS Id,
                        c.cli_nome AS Nome,
                        c.cli_telefone AS Telefone,
                        p.valortotal AS ValorTotal,
                        pr.nome AS NomeProduto,
                        pp.quantidade AS QuantidadeVendidaParaProduto,
                        pp.valor AS ValorVendidaParaProduto
                    FROM
                        Pedidos p
                        INNER JOIN PedidosProdutos pp ON p.Id = pp.PedidoId
                        INNER JOIN Produtos pr ON pp.ProdutoId = pr.Id
                        INNER JOIN tb_clientes c ON p.clienteid = c.cli_id
                    GROUP BY
                        p.Id,
                        c.cli_nome,
                        c.cli_telefone,
                        p.valortotal
                    OFFSET @offset ROWS
                    FETCH NEXT @totalPage ROWS ONLY";

                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@offset", offset));
                command.Parameters.Add(new SqlParameter("@totalPage", totalPage));

                var relatorioEntity2 = new List<PedidoClienteSomadas>();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        var relatorio = new PedidoClienteSomadas
                        {
                            Id = result.GetInt32(0),
                            Nome = result.GetString(1),
                            Telefone = result.GetString(2),
                            ValorTotal = result.GetDouble(3),
                            QuantidadeSomadaProduto = result.GetInt32(4),
                            ValorSomadoProduto = result.GetDouble(5)
                        };

                        relatorioEntity2.Add(relatorio);
                    }
                }
            }
        }
        finally
        {
            connection.Close();
        }
        */

        return new RegistroPaginado<PedidoClienteSomadas>{
            Registros = lista,
            TotalPorPagina = totalPage,
            PaginaCorrente = (int)page,
            TotalRegistros = relatorioEntity.Count()
        };
    }


    public static List<Cliente> ClientesBlocante([FromServices] ClienteServico servico)
    {
        // Forma 1
        return servico.ObterTodosClientes();
    }

    public static List<Cliente> ClientesTread([FromServices] ClienteServico servico)
    {
        // Forma 2
        List<Cliente> clientes =  new List<Cliente>();

        Thread thread = new Thread(() =>
        {
            clientes = servico.ObterTodosClientes();
        });

        thread.Start();
        thread.Join(); // Aguarda a conclusão da thread

        return clientes;
    }


    public static async Task<List<Cliente>> ClientesAsync([FromServices] ClienteServico servico)
    {
        // Forma 3
        List<Cliente> clientes =  new List<Cliente>();
        clientes = await servico.ObterTodosClientesAsync();

        return clientes;
    }


    public static List<Cliente> ClientesMedodoComAsync([FromServices] ClienteServico servico)
    {
        // Forma 4
        List<Cliente> clientes = servico.ObterTodosClientesAsync().Result;
        return clientes;
    }


    public static List<Cliente> ClientesMedodoComAsyncTaskFrom([FromServices] ClienteServico servico)
    {
        // Forma 5
        List<Cliente> clientes = Task.FromResult(servico.ObterTodosClientesAsync().Result).Result;

        return clientes;
    }
}