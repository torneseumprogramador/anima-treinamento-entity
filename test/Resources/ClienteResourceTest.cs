using entity.Contexto;
using entity.Entidades;
using entity.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class ClienteResourceTest
{
    public ClienteResourceTest()
    {
        var contextoMock = new Mock<BancoDeDadosContexto>();
        this.servicoMock = new Mock<ClienteServico>(contextoMock.Object);
    }

    private Mock<ClienteServico> servicoMock = default!;


    [Fact]
    public void ClientesBlocante()
    {
        // Arrange
        var listaMockada = new List<Cliente>() {
            new Cliente()
        };
        servicoMock.Setup(s => s.ObterTodosClientes()).Returns(listaMockada);

        // Act
        var clientes =  servicoMock.Object.ObterTodosClientes();

        // Assert
        Assert.Equal(1, clientes.Count);
    }
}