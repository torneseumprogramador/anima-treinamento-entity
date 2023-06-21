using System.Collections.Generic;
using System.Threading.Tasks;
using entity.Contexto;
using entity.Entidades;
using Microsoft.EntityFrameworkCore;

public class ClienteServico
{
    private readonly BancoDeDadosContexto _contexto;

    public ClienteServico(BancoDeDadosContexto contexto)
    {
        _contexto = contexto;
    }

    public List<Cliente> ObterTodosClientes()
    {
        return _contexto.Clientes.ToList();
    }

    public async Task<List<Cliente>> ObterTodosClientesAsync()
    {
        return await _contexto.Clientes.ToListAsync();
    }

    public Cliente? ObterClientePorId(int id)
    {
        return _contexto.Clientes.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Cliente?> ObterClientePorIdAsync(int id)
    {
        return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AdicionarCliente(Cliente cliente)
    {
        _contexto.Clientes.Add(cliente);
        await _contexto.SaveChangesAsync();
    }
}
