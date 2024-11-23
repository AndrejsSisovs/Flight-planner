using FluentValidation;
using WebApplication1.models;

namespace WebApplication1.Validations
{
    public class SearchrequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public SearchrequestValidator()
        {
            RuleFor(flight => flight.From).NotNull().NotEmpty();
            RuleFor(flight => flight.To).NotNull().NotEmpty();
            RuleFor(flight => flight.DepartureDate).NotNull().NotEmpty();
            RuleFor(flight => flight)
                .Must(f => !string.Equals(f.From.Trim().ToLower(), f.To.Trim().ToLower()));
        }
    }
}
