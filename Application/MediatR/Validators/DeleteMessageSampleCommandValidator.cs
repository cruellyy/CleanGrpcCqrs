using Application.MediatR.Commands.MessageSamples;
using FluentValidation;

namespace Application.MediatR.Validators;

public class DeleteMessageSampleCommandValidator : AbstractValidator<DeleteMessageSampleCommand>
{
    public DeleteMessageSampleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Требуется идентификатор записи.");
    }
}