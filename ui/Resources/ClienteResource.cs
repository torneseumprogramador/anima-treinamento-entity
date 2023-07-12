using entity.Contexto;
using entity.Entidades;
using entity.ModelViews;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;

public class ClienteResource
{
    public static IResult ClienteComPedido( [FromServices] IClienteServico servico, [FromQuery] int? page)
    {
        var retorno = servico.AulaEntityFramework(page);
        return Results.Json(retorno, null, null, 200);
    }

    public static IResult ClientesBlocante([FromServices] IClienteServico servico)
    {
        // Forma 1
        var retorno = servico.ObterTodosClientes();
        return Results.Json(retorno, null, null, 200);
    }

    public static async Task<IResult> CadastrarCliente([FromServices] IClienteServico servico, [FromBody] Cliente cliente)
    {
        await servico.AdicionarCliente(cliente);

        return Results.Created("/clientes", cliente);
    }


    public static IResult ClientesTread([FromServices] IClienteServico servico)
    {
        // Forma 2
        List<Cliente> clientes =  new List<Cliente>();

        Thread thread = new Thread(() =>
        {
            clientes = servico.ObterTodosClientes();
        });

        thread.Start();
        thread.Join(); // Aguarda a conclusão da thread

        return Results.Json(clientes, null, null, 200);
    }


    public static async Task<IResult> ClientesAsync([FromServices] IClienteServico servico)
    {
        // Forma 3
        List<Cliente> clientes =  new List<Cliente>();
        clientes = await servico.ObterTodosClientesAsync();

        return Results.Json(clientes, null, null, 200);
    }


    public static IResult ClientesMedodoComAsync([FromServices] IClienteServico servico)
    {
        // Forma 4
        List<Cliente> clientes = servico.ObterTodosClientesAsync().Result;
        return Results.Json(clientes, null, null, 200);
    }


    public static IResult ClientesMedodoComAsyncTaskFrom([FromServices] IClienteServico servico)
    {
        // Forma 5
        List<Cliente> clientes = Task.FromResult(servico.ObterTodosClientesAsync().Result).Result;

        return Results.Json(clientes, null, null, 200);
    }
}