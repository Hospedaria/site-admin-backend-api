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

app.MapGet("/exportar/reservas", async ([FromQuery] DateTime dataReferencia,
    IDiaReservaService diaReservaService,
    IReservaService reservaService,
    IPagamentoService pagamentoService) =>
{
    return await ExportarReservas.Exportar(dataReferencia, diaReservaService, reservaService,pagamentoService);
});

app.MapPost("/reservas", async ([FromBody] Reserva reserva,
    IReservaService reservaService,
    IDiaReservaService diaReservaService,
    IValidator<Reserva> validator) =>
{
    return await CadastraReservaHandler.CadastrarReserva(reserva, reservaService, diaReservaService, validator);
});

app.MapGet("/reservas/apartirdehoje", async (IReservaService reservaService, IDiaReservaService diaReservaService, IPagamentoService pagamentoService) =>
{
    return await ConsultaReservasAPartirDeHoje.Consultar(reservaService, diaReservaService, pagamentoService);
});

app.MapGet("/reservas/{id:guid}", async(Guid id,
    IReservaService reservaService,
    IPagamentoService pagamentoService) =>
{
    return await BuscaReservaHandler.BuscarReserva(id, reservaService, pagamentoService);
});

app.MapPost("/reservas/{id:guid}/pagamentos", async ([FromBody] Pagamento pagamento,
    IValidator<Pagamento> validator,
    IPagamentoService pagamentoService) =>
{
    return await CadastraPagamentoHandler.CadastrarPagamento(pagamento, validator,pagamentoService);
});

app.MapDelete("/reservas/{id:guid}/pagamentos/{idPagamento:guid}", async (Guid id, Guid idPagamento,
    IPagamentoService pagamentoService) =>
{
    return await DeletaPagamentoHandler.DeletaPagamento(id, idPagamento, pagamentoService);
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
    IDiaReservaService diaReservaService,
    IPagamentoService pagamentoService) =>
{
    return await DeletarReservaHandler.DeletarReserva(id, reservaService, diaReservaService,
        pagamentoService);
});

app.MapGet("/reservas/consultaPorPeriodo", async ([FromQuery(Name = "datainicio")]DateTime dataInicio,
    [FromQuery(Name = "datatermino")] DateTime dataTermino,
    IReservaService reservaService,
    IDiaReservaService diaReservaService,
    IPagamentoService pagamentoService) =>
{
    return await ConsultaPorPeriodo.Consultar(dataInicio, dataTermino, reservaService, diaReservaService,
        pagamentoService);
});

app.UseCors("HospedagemReservasPolicy");

app.Run();