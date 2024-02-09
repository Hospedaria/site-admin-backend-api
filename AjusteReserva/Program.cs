using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Interfaces;
using Hospedaria.Reservas.Api.Services;

var client = new AmazonDynamoDBClient();
var context = new DynamoDBContext(client);

Environment.SetEnvironmentVariable("TB_RESERVAS", "tbes_reservas");
Environment.SetEnvironmentVariable("TB_DIAS_RESERVAS", "tbes_dias_reservas");

var reservas = await context.ScanAsync<Reserva>(new List<ScanCondition>(), new DynamoDBOperationConfig()
{
    OverrideTableName = Reserva.GetNomeTabela()
}).GetRemainingAsync();

IDiaReservaService diasReservas = new DiaReservaService(client);

reservas.ForEach(async (reserva) =>
{
    await diasReservas.CadastrarDias(reserva);
});

Console.ReadKey();
