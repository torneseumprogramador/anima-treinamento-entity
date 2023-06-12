using entity.Contexto;
using Microsoft.EntityFrameworkCore;
using entity.Entidades;
using Microsoft.AspNetCore.Mvc;
using entity.ModelViews;

public class Console
{
    public static void Execute()
    {
        // #### Uso da estratégia 1 ####
        var contexto = new BancoDeDadosContexto();

        // ##### exemplo inserir dados

        var cliente = new Cliente
        {
            //Id = 1,
            Nome = "William",
            Telefone = "(22)2222-2222",
            Observacao = "Um teste"
        };

        var clienteMatheus = new Cliente
        {
            //Id = 1,
            Nome = "Matheus",
            Telefone = "(22)2222-2222",
            Observacao = "Um teste"
        };

        contexto.Clientes.Add(cliente);
        contexto.Clientes.Add(clienteMatheus);

        contexto.SaveChanges();


        /*
        var cliente = new Cliente
        {
            Id = 1,
            Nome = "William",
            Telefone = "(22)2222-2222",
            Observacao = "Um teste"
        };

        contexto.Clientes.Attach(cliente);

        contexto.Entry(cliente).State = EntityState.Modified;

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
    }
}