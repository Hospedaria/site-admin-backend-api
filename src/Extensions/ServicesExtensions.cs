using Amazon.DynamoDBv2;
using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;
using Hospedaria.Reservas.Api.Services;
using Hospedaria.Reservas.Api.Validations;

namespace Hospedaria.Reservas.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegistrarServicos(this IServiceCollection services)
        {
            return RegistrarServicosAWS(services)
                .AddSingleton<IReservaService, ReservaService>()
                .AddSingleton<IDiaReservaService, DiaReservaService>()
                .AddScoped<IValidator<Reserva>, ReservaValidator>();
        }

        private static IServiceCollection RegistrarServicosAWS(IServiceCollection services)
        {
            return services
                .AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();

        }

        public static IServiceCollection ConfigurarCors(this IServiceCollection services)
        {
            string? origensParametro = Environment.GetEnvironmentVariable("ORIGINS");

            string[] origins = string.IsNullOrEmpty(origensParametro) ?
                new[] { "*" } : origensParametro.Split(";");

            services.AddCors(options =>
            {
                options.AddPolicy("HospedagemReservasPolicy", builder =>
                    {
                        builder.WithOrigins(origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            return services;
        }
    }
}