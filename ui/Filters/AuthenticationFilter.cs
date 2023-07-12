#nullable disable

namespace entity.Filters
{
    public class AuthenticationFilter : IEndpointFilter
    {
        public virtual async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var validationUrl = $"{configuration["URLIdentityServer"]}/valid-token";
                
                var httpClient = new HttpClient();

                // Fazer a chamada HEAD
                var request = new HttpRequestMessage(HttpMethod.Head, validationUrl);
                request.Headers.Add("Authorization", $"Bearer {token}");
                try{
                    var response = await httpClient.SendAsync(request);

                    // Verificar o código de status da resposta
                    if (response.IsSuccessStatusCode)
                        return await next(context);
                }
                catch {}
            }

            return Results.Json(new { Mensagem = "Acesso negado, precisa de um token válido no header" }, null, null, 401);
        }
    }
}
