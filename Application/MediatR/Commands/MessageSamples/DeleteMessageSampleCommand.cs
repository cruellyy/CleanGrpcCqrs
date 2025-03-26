using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class DeleteMessageSampleCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}