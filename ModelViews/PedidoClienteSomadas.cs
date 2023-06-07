namespace entity.ModelViews;

public struct PedidoClienteSomadas
{
    public int Id {get;set;}
    public string Nome {get;set;}
    public string Telefone {get;set;}
    public double ValorTotal {get;set;}
    public int QuantidadeSomadaProduto {get;set;}
    public double ValorSomadoProduto {get;set;}
}