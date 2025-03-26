using Application.MediatR.Commands.MessageSamples;
using FluentValidation;

namespace Application.MediatR.Validators;

public class InfoMessageSampleCommandValidator : AbstractValidator<EditMessageSampleCommand>
{
    public InfoMessageSampleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Требуется идентификатор записи.");
    }
}