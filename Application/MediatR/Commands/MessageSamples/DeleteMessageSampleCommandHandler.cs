using Domain.Entities;
using Domain.Repositories;
using Grpc.Core;
using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class DeleteMessageSampleCommandHandler(
    IBaseRepository<MessageSample> messageSampleRepository)
    : IRequestHandler<DeleteMessageSampleCommand, int>
{
    public async Task<int> Handle(DeleteMessageSampleCommand request, CancellationToken cancellationToken)
    {
        var entity = await messageSampleRepository.GetByIdAsync(request.Id)
                     ?? throw new RpcException(
                         new Status(StatusCode.NotFound, "Шаблон сообщения не найден или удален."));
        
        await messageSampleRepository.DeleteAsync(entity);
        
        return entity.Id;
    }
}