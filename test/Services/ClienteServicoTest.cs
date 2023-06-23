
using entity.Contexto;
using entity.Entidades;
using Microsoft.EntityFrameworkCore;
// using test.Mocks;
using Moq;

namespace test;

public class ClienteServicoTest
{
    public ClienteServicoTest()
    {
        contextoMock = new Mock<BancoDeDadosContexto>();
        mockDbSet = new Mock<DbSet<Cliente>>();
        servico = new ClienteServico(contextoMock.Object); // TODO interface Mocada de contexto
    }

    private ClienteServico servico = default!;
    private Mock<BancoDeDadosContexto> contextoMock = default!;
    private Mock<DbSet<Cliente>> mockDbSet;

    [Fact]
    public void TestandoContrutor()
    {
        Assert.NotNull(servico);
    }

    [Fact]
    public void ObterTodosClientes()
    {   
        // Arrange
        var dados = new List<Cliente>();
        mockDbSetAction(dados);

        contextoMock.Setup(x => x.Clientes).Returns(mockDbSet.Object); // Utilize um método virtual

        var clientes = servico.ObterTodosClientes();
        Assert.Equal(0, clientes.Count);
    }

    [Fact]
    public void ObterUmClientes()
    {   
        // Arrange
        var dados = new List<Cliente>(){
            new Cliente()
        };
        mockDbSetAction(dados);
        contextoMock.Setup(x => x.Clientes).Returns(mockDbSet.Object); // Utilize um método virtual

        // Act
        var clientes = servico.ObterTodosClientes();

        // Assert
        Assert.Equal(1, clientes.Count);
    }

    private void mockDbSetAction(List<Cliente> lista)
    {
        var dados = lista.AsQueryable();
        mockDbSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(dados.Provider);
        mockDbSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(dados.Expression);
        mockDbSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(dados.ElementType);
        mockDbSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(dados.GetEnumerator());
    }


    [Fact]
    public void ObterClientePorIdNaoExistente()
    {
        // Arrange
        var dados = new List<Cliente>(){};
        mockDbSetAction(dados);
        contextoMock.Setup(x => x.Clientes).Returns(mockDbSet.Object); // Utilize um método virtual


        var cliente = servico.ObterClientePorId(1);
        Assert.Equal(null, cliente);
    }

    [Fact]
    public void ObterClientePorIdExistente()
    {
        // Arrange
        var dados = new List<Cliente>(){
            new Cliente() {
                Id = 1,
                Nome = "João Victor"
            }
        };
        mockDbSetAction(dados);
        contextoMock.Setup(x => x.Clientes).Returns(mockDbSet.Object); // Utilize um método virtual
            
        var cliente = servico.ObterClientePorId(1);
        Assert.NotNull(cliente);
        Assert.Equal("JOÃO VICTOR", cliente.Nome);
    }
}