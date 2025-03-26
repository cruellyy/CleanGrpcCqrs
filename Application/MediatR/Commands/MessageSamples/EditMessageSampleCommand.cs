using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class EditMessageSampleCommand(string title, string content, int id) : IRequest<int>
{
    public int Id { get; set; } = id;
    public string? Title { get; set; } = title;
    public string? Content { get; set; } = content;
}