using Application.MediatR.Commands.MessageSamples;
using FluentValidation;

namespace Application.MediatR.Validators;

public class CreateMessageSampleCommandValidator : AbstractValidator<CreateMessageSampleCommand>
{
    public CreateMessageSampleCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Требуется заголовок сообщения.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Требуется шаблон сообщения.");
    }
}