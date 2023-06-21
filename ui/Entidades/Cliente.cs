using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using entity.Patterns;

namespace entity.Entidades;

public record Cliente
{
    public int Id {get;set;}

    private string nome = default!;
    public string Nome 
    {
        get{ return (this.nome == null ? "" : this.nome).ToUpper(); }
        set{ this.nome = value; }
    }

    public string Telefone {get;set;} = default!;

    public string Observacao { get; set; } = default!;

    [NotMapped]
    public CPF Cpf { get; set; } = default!;
}