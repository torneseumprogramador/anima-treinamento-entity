using entity.Contexto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace entity;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get;set; }

    public void ConfigureServices(IServiceCollection services)
    {

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // services.AddScoped<IBancoDeDadosContexto, BancoDeDadosContexto>();

        services.AddScoped<ClienteServico>();
        // estratégia 2 adicionando sobre injeção de dependência
        services.AddDbContext<BancoDeDadosContexto>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("conexao"),
                new MySqlServerVersion(new Version(8, 0, 21)));
        });
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            Routes.Registar(endpoints);
        });
    }
}
