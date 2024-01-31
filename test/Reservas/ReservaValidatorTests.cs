using FluentValidation;
using FluentValidation.TestHelper;
using Hospedaria.Reservas.Api.Entities;
using Hospedaria.Reservas.Api.Validations;

namespace Hospedaria.Reservas.Api.Tests.Reservas
{
    [TestClass]
    public class ReservaValidatorTests
    {
        Reserva ReservaNok {get;set;}
        IValidator<Reserva> Validator {get;set;}

        public ReservaValidatorTests()
        {
            Validator = new ReservaValidator();
            ReservaNok = new();
        }

        [TestMethod]
        public void DeveEstourarErroIdVazio(){
            ReservaNok.Id = Guid.Empty.ToString();
            
            Validator.TestValidate(ReservaNok)
                .ShouldHaveValidationErrorFor(c => c.Id)
                .WithErrorMessage("Id da reserva é obrigatório");
        }
    }
}