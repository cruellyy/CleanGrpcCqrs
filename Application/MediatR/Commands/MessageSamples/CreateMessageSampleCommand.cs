using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class CreateMessageSampleCommand(string title, string content) : IRequest<int>
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
}