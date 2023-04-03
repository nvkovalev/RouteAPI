using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;

namespace RouteAPI.Pipelines
{
    public class ValidationBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var errors = new List<ValidationFailure>();
            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);
                if (!result.IsValid)
                    errors.AddRange(result.Errors);
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }
    }
}
