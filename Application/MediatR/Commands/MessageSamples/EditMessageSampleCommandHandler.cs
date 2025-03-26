using Domain.Entities;
using Domain.Repositories;
using Grpc.Core;
using MediatR;

namespace Application.MediatR.Commands.MessageSamples;

public class EditMessageSampleCommandHandler(
    IBaseRepository<MessageSample> messageSampleRepository)
    : IRequestHandler<EditMessageSampleCommand, int>
{
    public async Task<int> Handle(EditMessageSampleCommand request, CancellationToken cancellationToken)
    {
        var messageSample = await messageSampleRepository.GetByIdAsTrackingAsync(request.Id)
                            ?? throw new RpcException(new Status(StatusCode.NotFound,
                                "Шаблон сообщения не найден или удален."));

        messageSample.Title = string.IsNullOrEmpty(request.Title) ? messageSample.Title : request.Title;
        messageSample.Content = string.IsNullOrEmpty(request.Content) ? messageSample.Content : request.Content;

        messageSampleRepository.Update(messageSample);
        await messageSampleRepository.SaveTransationAsync();

        return messageSample.Id;
    }
}