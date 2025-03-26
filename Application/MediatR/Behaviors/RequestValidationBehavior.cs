using FluentValidation;
using MediatR;

namespace Application.MediatR.Behaviors;

public class RequestValidationBehavior<TRequest, TResponse>(IValidatorFactory validatorFactory)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validator = validatorFactory.GetValidator<TRequest>();

        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        return await next();
    }
}