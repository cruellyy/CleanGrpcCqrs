using Application.Dtos;
using MediatR;

namespace Application.MediatR.Queries.MessageSample;

public class InfoMessageSampleQuery(int id) : IRequest<MessageSampleDto>
{
    public int Id { get; set; } = id;
}