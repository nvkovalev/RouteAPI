using FluentValidation;
using RouteAPI.Requests;
using RouteAPI.Resources;

namespace RouteAPI.Validators
{
    public class SearchRequestValidator : AbstractValidator<SearchRequestMain>
    {
        public SearchRequestValidator()
        {
            RuleFor(m => m.Origin)
               .NotEmpty().WithMessage(x => String.Format(ErrorMessage.IsRequired, nameof(x.Origin)));

            RuleFor(m => m.Destination)
               .NotEmpty().WithMessage(x => String.Format(ErrorMessage.IsRequired, nameof(x.Destination)));
        }
    }
}
