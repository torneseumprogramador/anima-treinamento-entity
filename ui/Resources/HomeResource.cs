using entity.Contexto;
using entity.Entidades;
using Microsoft.AspNetCore.Mvc;

public class HomeResource
{
    public static List<Cliente> Index([FromServices] IClienteServico servico)
    {
        // return new StatusCodeResult(404);
        // return Results.NotFound(new { Mensagem = "Não encontrado" });

        // return new StatusCodeResult(404, new { Mensagem = "Não encontrado" });
        return servico.ObterTodosClientes();
    }
}