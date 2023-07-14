using entity.Filters;

public class Routes
{
    public static void Registar(IEndpointRouteBuilder app)
    {
        #region weatherforecast
        app.MapGet("/weatherforecast", WeatherforecastResource.Index)
            .AddEndpointFilter<AuthenticationFilter>()
            .WithName("GetWeatherForecast").WithOpenApi();

        #endregion

        #region Home
        app.MapGet("/", HomeResource.Index).WithName("Home").WithOpenApi();
        #endregion

        #region Clientes
        app.MapGet("/clientes-com-pedidos", ClienteResource.ClienteComPedido).WithOpenApi().AddEndpointFilter<AuthenticationFilter>();
        
        
        app.MapPost("/clientes/fila", ClienteResource.MandarMensagemFila).WithOpenApi();
       
        app.MapPost("/clientes", ClienteResource.CadastrarCliente).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();

        // Forma 1 - Não paralelo
        app.MapGet("/clientes-blocante", ClienteResource.ClientesBlocante).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();

        // Forma 2
        app.MapGet("/clientes-tread", ClienteResource.ClientesTread).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();

        // Forma 3
        app.MapGet("/clientes-async", ClienteResource.ClientesAsync).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();

        // Forma 4
        app.MapGet("/clientes-metodo-com-async", ClienteResource.ClientesMedodoComAsync).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();

        // Forma 5
        app.MapGet("/clientes-metodo-com-async-task-from", ClienteResource.ClientesMedodoComAsyncTaskFrom).AddEndpointFilter<AuthenticationFilter>().WithOpenApi();
        #endregion
    }
}