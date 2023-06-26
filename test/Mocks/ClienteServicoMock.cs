using entity.Entidades;
using entity.ModelViews;

namespace test.Mocks;

public class ClienteServicoMock : IClienteServico
{
    private static List<Cliente> clientes = new List<Cliente>();

    public async Task AdicionarCliente(Cliente cliente)
    {
        cliente.Id = clientes.Count + 1;
        await Task.Run(() => clientes.Add(cliente));
    }

    public RegistroPaginado<PedidoClienteSomadas> AulaEntityFramework(int? page)
    {
        var paginado = new RegistroPaginado<PedidoClienteSomadas>();
        return paginado;
    }

    public Cliente? ObterClientePorId(int id)
    {
        return clientes.Find(c => c.Id == id);
    }

    public async Task<Cliente?> ObterClientePorIdAsync(int id)
    {
        return await Task.Run(() => clientes.Find(c => c.Id == id) );
    }

    public List<Cliente> ObterTodosClientes()
    {
        return clientes;
    }

    public async Task<List<Cliente>> ObterTodosClientesAsync()
    {
        return await Task.Run(() => clientes );
    }
}