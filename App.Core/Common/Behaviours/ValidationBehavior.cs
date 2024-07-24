using App.Domain.Models.shared;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace App.Core.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ResponseResult, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<string> errors = new List<string>();
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

                ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                
                if (failures.Count() != 0)
                {
                    foreach (var item in failures)
                    {
                        errors.Add(item.ErrorMessage);
                    }
                    return new TResponse
                    {
                        result = enums.Result.failed,
                        note = string.Join('-', errors)
                    };
                }
                
            }
            try
            {
                return await next();

            }
            catch (Exception ex)
            {

                return new TResponse
                {
                    result = enums.Result.failed,
                    note = ex.Message
                };
            }
        }

    }

}
 