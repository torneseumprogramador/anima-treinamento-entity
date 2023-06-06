using entity.Contexto;
using Microsoft.EntityFrameworkCore;
using entity.Entidades;
using Microsoft.AspNetCore.Mvc;


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




app.MapGet("/clientes-com-pedidos", ([FromServices] BancoDeDadosContexto contexto) =>
{
    var lista = contexto.Pedidos.Include(p => p.Cliente).Select( p =>  new { 
        cli_nome = p.Cliente.Nome,
        cli_telefone = p.Cliente.Telefone,
        valortotal = p.ValorTotal
    })
    .Skip(0)
    .Take(10).ToList();

    // foreach(var item in lista)
    // {
    //     Console.WriteLine(item.);
    // }

    return lista;
})
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
