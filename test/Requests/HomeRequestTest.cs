using System;
using entity.Entidades;
using Newtonsoft.Json;

namespace test.Requests;

public class HomeRequestTest
{
    [Fact]
    public async void TestandoContrutor()
    {
        // Arrage 
        HttpClient client = new HttpClient();
        string url = "http://localhost:5096/";
        HttpResponseMessage response = await client.GetAsync(url);
        
        // Act
        string content = await response.Content.ReadAsStringAsync();

        var clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);

        // Assert
        Assert.True(clientes.Count > 0);
    }
}