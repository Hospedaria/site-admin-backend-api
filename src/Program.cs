using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Handlers;
using Hospedaria.Reservas.Api.Interfaces;
using Hospedaria.Reservas.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigurarCors()
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi)
    .RegistrarServicos();

var app = builder.Build();

app.MapPost("/reservas", async ([FromBody] Reserva reserva,
    IReservaService reservaService,
    IValidator<Reserva> validator) =>
{
    return await CadastraReservaHandler.CadastrarReserva(reserva, reservaService, validator);
});

app.MapGet("/reservas/apartirdehoje", async (IReservaService reservaService) =>
{
    return await ConsultaReservasAPartirDeHoje.Consultar(reservaService);
});

app.MapGet("/reservas/{id:guid}", async(Guid id,
    IReservaService reservaService) =>
{
    return await BuscaReservaHandler.BuscarReserva(id, reservaService);
});

app.MapPut("/reservas/{id:guid}", async (Guid id,
    [FromBody] Reserva reserva,
    IReservaService reservaService) =>
{
    return await AtualizarReservaHandler.AtualizarReserva(id, reserva, reservaService);
});

app.MapDelete("/reservas/{id:guid}", async (Guid id,
    IReservaService reservaService) =>
{
    return await DeletarReservaHandler.DeletarReserva(id, reservaService);
});

app.MapGet("/reservas/consultaPorData/{data:datetime}", async (DateTime data,
    IReservaService reservaService) =>
{
    return await ConsultaPorDataHandler.Consultar(data, reservaService);
});

app.UseCors("HospedagemReservasPolicy");

app.Run();