using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entity.Entidades;

public record Pedido
{
    public int Id {get;set;}
    public int ClienteId {get;set;}
    public Cliente Cliente {get;set;} = default!;
    public double ValorTotal {get;set;}
    public DateTime Data {get;set;}
}