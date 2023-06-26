public class Routes
{
    public static void Registar(IEndpointRouteBuilder app)
    {
        #region weatherforecast
        app.MapGet("/weatherforecast", WeatherforecastResource.Index).WithName("GetWeatherForecast").WithOpenApi();
        #endregion


        #region Home
        app.MapGet("/", HomeResource.Index).WithName("Home").WithOpenApi();
        #endregion

        #region Clientes
        app.MapGet("/clientes-com-pedidos", ClienteResource.ClienteComPedido).WithOpenApi();
        
        app.MapPost("/clientes", ClienteResource.CadastrarCliente).WithOpenApi();

        // Forma 1 - Não paralelo
        app.MapGet("/clientes-blocante", ClienteResource.ClientesBlocante).WithOpenApi();

        // Forma 2
        app.MapGet("/clientes-tread", ClienteResource.ClientesTread).WithOpenApi();

        // Forma 3
        app.MapGet("/clientes-async", ClienteResource.ClientesAsync).WithOpenApi();

        // Forma 4
        app.MapGet("/clientes-metodo-com-async", ClienteResource.ClientesMedodoComAsync).WithOpenApi();

        // Forma 5
        app.MapGet("/clientes-metodo-com-async-task-from", ClienteResource.ClientesMedodoComAsyncTaskFrom).WithOpenApi();
        #endregion
    }
}