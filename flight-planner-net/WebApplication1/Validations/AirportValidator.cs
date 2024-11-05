using FlightPlanner.Core.Models;
using FluentValidation;

namespace WebApplication1.Validations
{
    public class AirportValidator : AbstractValidator<Airport>
    {
        public AirportValidator() 
        {
            RuleFor(airport => airport.Id).NotNull().NotEmpty();
            RuleFor(airport => airport.AirportCode).NotNull().NotEmpty();
            RuleFor(airport => airport.City).NotNull().NotEmpty();
            RuleFor(airport => airport.Country).NotNull().NotEmpty();
        }

    }
}
