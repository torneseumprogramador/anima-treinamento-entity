using entity.Entidades;
using entity.ModelViews;

public interface IClienteServico
{
    List<Cliente> ObterTodosClientes();

    Task<List<Cliente>> ObterTodosClientesAsync();

    Cliente? ObterClientePorId(int id);

    Task<Cliente?> ObterClientePorIdAsync(int id);

    Task AdicionarCliente(Cliente cliente);

    RegistroPaginado<PedidoClienteSomadas> AulaEntityFramework(int? page);
}
