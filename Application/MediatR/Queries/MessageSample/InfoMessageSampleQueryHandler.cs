using Application.Dtos;
using AutoMapper;
using Domain.Repositories;
using Grpc.Core;
using MediatR;

namespace Application.MediatR.Queries.MessageSample;

public class InfoMessageSampleQueryHandler(IBaseRepository<Domain.Entities.MessageSample> messageSampleRepository,
    IMapper mapper)
    : IRequestHandler<InfoMessageSampleQuery, MessageSampleDto>
{
    public async Task<MessageSampleDto> Handle(InfoMessageSampleQuery request, CancellationToken cancellationToken)
    {
        var entity = await messageSampleRepository.GetByIdAsync(request.Id)
                     ?? throw new RpcException(
                         new Status(StatusCode.NotFound, "Шаблон сообщения не найден или удален."));
        
        return mapper.Map<MessageSampleDto>(entity);
    }
}