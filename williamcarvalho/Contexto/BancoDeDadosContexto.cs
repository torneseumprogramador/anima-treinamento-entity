using Microsoft.EntityFrameworkCore;
using williamcarvalho.Entidades;

namespace williamcarvalho.Contexto;

public class BancoDeDadosContexto : DbContext
{ 
    public BancoDeDadosContexto(DbContextOptions<BancoDeDadosContexto> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = default!;
}