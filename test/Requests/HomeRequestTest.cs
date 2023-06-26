using System;
using System.Net.Http.Json;
using entity.Entidades;
using entity.Patterns;
using Newtonsoft.Json;

namespace test.Requests;

public class HomeRequestTest
{
    [Fact]
    public async void TestandoContrutor()
    {
        // Arrage 
        Setup.ClassInit(); // startando app no ambiente de teste

        HttpClient client = Setup.client;
        string url = "/";
        HttpResponseMessage response = await client.GetAsync(url);
        
        // Act
        string content = await response.Content.ReadAsStringAsync();

        var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);

        // Assert
        Assert.Equal(0, clientes.Count);

        Setup.ClassCleanup();
    }


    [Fact]
    public async void CriarRegistroNaAPI()
    {
        // Arrage 
        Setup.ClassInit(); // startando app no ambiente de teste

        HttpClient client = Setup.client;

        // Act
        var clienteCriado = await cadastraCliente(client);

        // Assert
        Assert.Equal(1, clienteCriado?.Id);
        Assert.Equal("CAIO", clienteCriado?.Nome);

        Setup.ClassCleanup();
    }

    [Fact]
    public async void CriarRegistroNaAPIEConsultar()
    {
        // Arrage 
        Setup.ClassInit(); // startando app no ambiente de teste

        HttpClient client = Setup.client;

        // Act
        await cadastraCliente(client);
        

        HttpResponseMessage response = await client.GetAsync("/clientes-blocante");
        
        // Act
        string content = await response.Content.ReadAsStringAsync();

        var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);

        // Assert
        Assert.Equal(1, clientes.Count);

        Setup.ClassCleanup();
    }

    private async Task<Cliente?> cadastraCliente(HttpClient client)
    {
        var cliente = new Cliente(){
            Nome = "Caio",
            Cpf = new CPF("869.088.920-50"),
            Observacao = "Um teste",
            Telefone = "(11) 99999-0000"
        };
        
        // Act
        var responseCreate = await client.PostAsJsonAsync("/clientes", cliente);
        var clienteCriado = await responseCreate.Content.ReadFromJsonAsync<Cliente>();
        return clienteCriado;
    }
}