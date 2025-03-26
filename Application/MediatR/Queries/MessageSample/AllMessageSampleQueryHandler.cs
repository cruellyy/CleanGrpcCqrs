using Application.Dtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.Queries.MessageSample;

public class AllMessageSampleQueryHandler(
    IBaseRepository<Domain.Entities.MessageSample> messageSampleRepository,
    IMapper mapper)
    : IRequestHandler<AllMessageSampleQuery, List<MessageSampleDto>>
{
    public async Task<List<MessageSampleDto>> Handle(AllMessageSampleQuery request, CancellationToken cancellationToken)
    {
        var entities = (await messageSampleRepository.GetAll()).Skip(request.Offset ?? 0)
            .Take(request.Count is null or 0 ? 20 : (int)request.Count)
            .ToList();
        
        return mapper.Map<List<MessageSampleDto>>(entities);
    }
}