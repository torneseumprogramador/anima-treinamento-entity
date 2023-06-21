using entity.Entidades;
using entity.Patterns;

namespace test;

public class ClienteTest
{
    [Fact]
    public void TestandoContrutor()
    {
        // Arrage & Act
        var cliente = new Cliente();

        // Assert
        Assert.NotNull(cliente);
    }


    [Fact(DisplayName = "Um teste de propriedades do obj")]
    public void TestandoPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        //Act - testando set
        cliente.Id = 1;
        cliente.Nome = "xxx";
        cliente.Telefone = "xxxx";
        cliente.Observacao = "xxxx";

        // Assert- testando get
        Assert.Equal(1, cliente.Id);
        Assert.Equal("XXX", cliente.Nome);
        Assert.Equal("xxxx", cliente.Telefone);
        Assert.Equal("xxxx", cliente.Observacao);
    }


    [Fact(DisplayName = "Passando null no nome")]
    public void TestandoErroPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        //Act - testando set
        cliente.Nome = null;

        // Assert- testando get
        Assert.Equal("", cliente.Nome);
    }

    [Fact(DisplayName = "Atribuir CPF nulo deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFNullPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF(null));

        // Assert
        Assert.Equal("CPF não pode ser null", exception.Message);
    }

    [Fact(DisplayName = "Atribuir CPF empty deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFEmptyPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF(""));

        // Assert
        Assert.Equal("CPF não pode ter menos que 11 caracteres", exception.Message);
    }

    
    [Fact(DisplayName = "Atribuir CPF com mais de 11 caracteres deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFMaisDe11Propriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF("322323232322323"));

        // Assert
        Assert.Equal("CPF não pode ter menos que 11 caracteres", exception.Message);
    }

    [Fact(DisplayName = "Atribuir CPF inválido deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFInvalidoPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF("22222222222"));

        // Assert
        Assert.Equal("CPF inválido", exception.Message);
    }

    [Fact(DisplayName = "Atribuir CPF inválido 000 deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFInvalido000Propriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF("00000000000"));

        // Assert
        Assert.Equal("CPF inválido", exception.Message);
    }

    [Fact(DisplayName = "Atribuir CPF inválido deve lançar uma exceção com mensagem de erro correta")]
    public void TestandoCPFInvalidosPropriedades()
    {
        // Arrage 
        var cliente = new Cliente();

        // Act
        var exception = Assert.Throws<Exception>(() => cliente.Cpf = new CPF("32222333298"));

        // Assert
        Assert.Equal("CPF é inválido", exception.Message);
    }
}