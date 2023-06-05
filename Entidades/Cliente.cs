namespace entity.Entidades;

public record Cliente
{
    public int Id {get;set;}
    public string Nome {get;set;} = default!;
    public string Telefone {get;set;} = default!;
}