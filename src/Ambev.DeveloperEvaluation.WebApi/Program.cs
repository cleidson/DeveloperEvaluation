using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.BranchesFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductsFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.SalesFeature;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application...");

            var builder = WebApplication.CreateBuilder(args);

            //  Configuração de Logs
            builder.AddDefaultLogging();

            //  Registrar todas as dependências primeiro
            builder.RegisterDependencies();

            //  Configuração do Banco de Dados
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );
             

            //  FluentValidation (Validadores)
            builder.Services.AddScoped<IValidator<AuthenticateUserRequest>, AuthenticateUserRequestValidator>();
            builder.Services.AddScoped<IValidator<SaleRequest>, SaleRequestValidator>();
            builder.Services.AddScoped<IValidator<ProductRequest>, ProductRequestValidator>();
            builder.Services.AddScoped<IValidator<BranchRequest>, BranchRequestValidator>();

            //  AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            //  MediatR (Handlers)
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            //  Middleware de Validação
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //  Configuração dos Endpoints e Health Checks
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.AddBasicHealthChecks();

            //  Criar a Aplicação
            var app = builder.Build();

            //  Rodar as migrations automaticamente antes de iniciar a API
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();

                try
                {
                    Log.Information("Applying database migrations...");
                    dbContext.Database.Migrate();
                    Log.Information("Database migrations applied successfully!");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error applying database migrations.");
                    throw;
                }
            }

            //  Middleware de Tratamento de Erros e Validações
            app.UseMiddleware<ValidationExceptionMiddleware>();

            //  Configuração do Ambiente de Desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //  Segurança e Autenticação
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            //  Health Checks
            app.UseBasicHealthChecks();

            //  Mapeamento de Controllers
            app.MapControllers();

            //  Executar a Aplicação
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, " Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
