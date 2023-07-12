using entity.Contexto;
using entity.Entidades;
using Microsoft.AspNetCore.Mvc;

public class HomeResource
{
    public static Object Index([FromServices] IClienteServico servico)
    {
        return new { Mensagem = "Bem vindo a API Minimal" };
    }
}