using Application.MediatR.Commands.MessageSamples;
using FluentValidation;

namespace Application.MediatR.Validators;

public class EditMessageSampleCommandValidator : AbstractValidator<EditMessageSampleCommand>
{
    public EditMessageSampleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Требуется идентификатор записи.");
    }
}