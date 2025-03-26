using Application.Dtos;
using MediatR;

namespace Application.MediatR.Queries.MessageSample;

public class AllMessageSampleQuery(int? count, int? offset) : IRequest<List<MessageSampleDto>>
{
    public int? Count { get; set; } = count;
    public int? Offset { get; set; } = offset;
}