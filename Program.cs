using entity.Contexto;
using Microsoft.EntityFrameworkCore;
using entity.Entidades;
using Microsoft.AspNetCore.Mvc;
using entity.ModelViews;


// #### Uso da estratégia 1 ####
// var contexto = new BancoDeDadosContexto();
/*
// ##### exemplo inserir dados

contexto.Clientes.Add(new Cliente{
    Nome = "William",
    Telefone = "(22)2222-2222"
});

contexto.SaveChanges();
*/
/*
// ##### exemplo lista dados
var clientes = contexto.Clientes.ToList();
var cliente = contexto.Clientes.Find(1);
*/

/*
// ##### exemplo update dados
var cliente = contexto.Clientes.First();
cliente.Nome = "William Cleisson";
contexto.Update(cliente);
contexto.SaveChanges();
*/

/*
// ##### exemplo excluir dados
contexto.Remove(new Cliente { Id = 1 });
contexto.SaveChanges();
*/


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// estratégia 2 adicionando sobre injeção de dependência
builder.Services.AddDbContext<BancoDeDadosContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("conexao"),
        new MySqlServerVersion(new Version(8, 0, 21)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapGet("/", ([FromServices] BancoDeDadosContexto contexto) =>
{
    // return new StatusCodeResult(404);
    // return Results.NotFound(new { Mensagem = "Não encontrado" });

    // return new StatusCodeResult(404, new { Mensagem = "Não encontrado" });
    return contexto.Clientes.ToList();
})
.WithName("Home")
.WithOpenApi();




app.MapGet("/clientes-com-pedidos", ([FromServices] BancoDeDadosContexto contexto, [FromQuery] int? page) =>
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
})
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
