
using entity.Contexto;
using entity.Entidades;
using Microsoft.EntityFrameworkCore;

namespace test;

public class ClienteServicoTest
{
    public ClienteServicoTest()
    {
        // 1 - // TODO interface Mocada de contexto
        // 2 - // TODO como mockar o entity
        
        contexto = new BancoDeDadosContexto("anima_dev_test");  // TODO como mockar o entity
        servico = new ClienteServico(contexto); // TODO interface Mocada de contexto
    }

    private ClienteServico servico = default!;
    private BancoDeDadosContexto contexto = default!;

    private void limpar()
    {
        var connection = contexto.Database.GetDbConnection();
        try
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var query = "delete from tb_clientes";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
        finally
        {
            connection.Close();
        }
    }
    

    [Fact]
    public void TestandoContrutor()
    {
        Assert.NotNull(servico);
    }

    [Fact]
    public void ObterTodosClientes()
    {
        limpar();
        var clientes = servico.ObterTodosClientes();
        Assert.Equal(0, clientes.Count);
    }

    [Fact]
    public void ObterClientePorIdNaoExistente()
    {
        var cliente = servico.ObterClientePorId(1);
        Assert.Equal(null, cliente);
    }

    [Fact]
    public async Task ObterClientePorIdExistente()
    {
        await servico.AdicionarCliente(new Cliente(){ Nome = "teste", Observacao = "ssss", Telefone = "1111" });

        var cliente = servico.ObterClientePorId(1);
        Assert.NotNull(cliente);
    }
}