using FluentValidation;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Extensions;
using Hospedaria.Reservas.Api.Handlers;
using Hospedaria.Reservas.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigurarCors()
    .AddAWSLambdaHosting(LambdaEventSource.HttpApi)
    .RegistrarServicos();

var app = builder.Build();

app.MapPost("/reservas", async ([FromBody] Reserva reserva,
    IReservaService reservaService,
    IDiaReservaService diaReservaService,
    IValidator<Reserva> validator) =>
{
    return await CadastraReservaHandler.CadastrarReserva(reserva, reservaService, diaReservaService, validator);
});

app.MapGet("/reservas/apartirdehoje", async (IReservaService reservaService, IDiaReservaService diaReservaService) =>
{
    return await ConsultaReservasAPartirDeHoje.Consultar(reservaService, diaReservaService);
});

app.MapGet("/reservas/{id:guid}", async(Guid id,
    IReservaService reservaService) =>
{
    return await BuscaReservaHandler.BuscarReserva(id, reservaService);
});

app.MapPut("/reservas/{id:guid}", async (Guid id,
    [FromBody] Reserva reserva,
    IReservaService reservaService,
    IDiaReservaService diaReservaService) =>
{
    return await AtualizarReservaHandler.AtualizarReserva(id, reserva, reservaService, diaReservaService);
});

app.MapDelete("/reservas/{id:guid}", async (Guid id,
    IReservaService reservaService,
    IDiaReservaService diaReservaService) =>
{
    return await DeletarReservaHandler.DeletarReserva(id, reservaService, diaReservaService);
});

app.MapGet("/reservas/consultaPorPeriodo", async ([FromQuery(Name = "datainicio")]DateTime dataInicio,
    [FromQuery(Name = "datatermino")] DateTime dataTermino,
    IReservaService reservaService,
    IDiaReservaService diaReservaService) =>
{
    return await ConsultaPorPeriodo.Consultar(dataInicio, dataTermino, reservaService, diaReservaService);
});

app.UseCors("HospedagemReservasPolicy");

app.Run();