using entity.Contexto;
using entity.Entidades;
using entity.ModelViews;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;


public class ClienteResource
{
    public static RegistroPaginado<PedidoClienteSomadas> ClienteComPedido([FromServices] IClienteServico servico, [FromQuery] int? page)
    {
        return servico.AulaEntityFramework(page);
    }

    public static List<Cliente> ClientesBlocante([FromServices] IClienteServico servico)
    {
        // Forma 1
        return servico.ObterTodosClientes();
    }

    public static async Task<IResult> CadastrarCliente([FromServices] IClienteServico servico, [FromBody] Cliente cliente)
    {
        await servico.AdicionarCliente(cliente);

        return Results.Created("/clientes", cliente);
    }


    public static List<Cliente> ClientesTread([FromServices] IClienteServico servico)
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


    public static async Task<List<Cliente>> ClientesAsync([FromServices] IClienteServico servico)
    {
        // Forma 3
        List<Cliente> clientes =  new List<Cliente>();
        clientes = await servico.ObterTodosClientesAsync();

        return clientes;
    }


    public static List<Cliente> ClientesMedodoComAsync([FromServices] IClienteServico servico)
    {
        // Forma 4
        List<Cliente> clientes = servico.ObterTodosClientesAsync().Result;
        return clientes;
    }


    public static List<Cliente> ClientesMedodoComAsyncTaskFrom([FromServices] IClienteServico servico)
    {
        // Forma 5
        List<Cliente> clientes = Task.FromResult(servico.ObterTodosClientesAsync().Result).Result;

        return clientes;
    }
}