using williamcarvalho.Contexto;
using williamcarvalho.Entidades;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BancoDeDadosContexto>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/clientes", async (BancoDeDadosContexto dbContext) =>
{
    var clientes = await dbContext.Clientes.ToListAsync();
    return clientes;
})
.WithName("GetClientes")
.WithOpenApi();

app.MapGet("/clientes/{id}", async (BancoDeDadosContexto dbContext, int id) =>
{
    var cliente = await dbContext.Clientes.FindAsync(id);
    return cliente;
})
.WithName("GetCliente")
.WithOpenApi();

app.MapPost("/clientes", async (BancoDeDadosContexto dbContext, Cliente cliente) =>
{
    dbContext.Clientes.Add(cliente);
    await dbContext.SaveChangesAsync();

    return cliente;
})
.WithName("CreateCliente")
.WithOpenApi();

app.MapPut("/clientes/{id}", async (BancoDeDadosContexto dbContext, int id, Cliente cliente) =>
{
    if (id != cliente.Id)
    {
        return StatusCodes.Status400BadRequest;
    }

    dbContext.Entry(cliente).State = EntityState.Modified;
    await dbContext.SaveChangesAsync();

    return StatusCodes.Status204NoContent;
})
.WithName("UpdateCliente")
.WithOpenApi();

app.MapDelete("/clientes/{id}", async (BancoDeDadosContexto dbContext, int id) =>
{
    var cliente = await dbContext.Clientes.FindAsync(id);
    if (cliente == null)
    {
        return StatusCodes.Status404NotFound;
    }

    dbContext.Clientes.Remove(cliente);
    await dbContext.SaveChangesAsync();

    return StatusCodes.Status204NoContent;
})
.WithName("DeleteCliente")
.WithOpenApi();

app.Run();