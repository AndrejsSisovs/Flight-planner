using FlightPlanner.Core.Models;
using FluentValidation;

namespace WebApplication1.Validations
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator() 
        {
            RuleFor(flight => flight.ArrivalTime)
                .Must(arrivalTime => !string.IsNullOrEmpty(arrivalTime))
                .When(flight => flight.ArrivalTime != null);

            RuleFor(flight => flight.DepartureTime).Must((flight, departureTime) =>
                DateTime.Parse(departureTime) < DateTime.Parse(flight.ArrivalTime))
                .When(flight => !string.IsNullOrEmpty(flight.ArrivalTime) && !string.IsNullOrEmpty(flight.DepartureTime));

            RuleFor(flight => flight.ArrivalTime).Must((flight, arrivalTime) =>
                DateTime.Parse(arrivalTime) > DateTime.Parse(flight.DepartureTime))
                .When(flight => !string.IsNullOrEmpty(flight.ArrivalTime) && !string.IsNullOrEmpty(flight.DepartureTime));

            RuleFor(flight => flight.DepartureTime).NotNull().NotEmpty();
            RuleFor(flight => flight.ArrivalTime).NotNull().NotEmpty();

            RuleFor(flight => flight.Carrier).NotNull().NotEmpty();
            RuleFor(flight => flight.From).NotNull().NotEmpty();
            RuleFor(flight => flight.To).NotNull().NotEmpty();
            RuleFor(flight => flight.From.AirportCode).NotNull().NotEmpty();
            RuleFor(flight => flight.To.AirportCode).NotNull().NotEmpty();


            RuleFor(flight => flight)
                .Must(f => !string.Equals(f.From.AirportCode?.Trim().ToLower(), f.To.AirportCode?.Trim().ToLower()));
        }
    }
}
