using Application.MediatR.Commands.MessageSamples;
using Application.MediatR.Queries.MessageSample;
using Grpc.Core;
using MediatR;

namespace NevaSms.Services;

/// <summary>
/// Сервис шаблонов сообщений
/// </summary>
/// <param name="mediator"></param>
public class MessageSampleServiceGrpc(IMediator mediator) : MessageSample.MessageSampleBase
{
    /// <summary>
    /// Создание нового шаблона сообщения
    /// </summary>
    /// <param name="request">Входящие параметры</param>
    /// <param name="context">Контекст</param>
    /// <returns></returns>
    public override async Task<MessageSampleId> Create(CreateMessageSampleRequest request,
        ServerCallContext context)
    {
        var command = new CreateMessageSampleCommand(request.Title, request.Content);
        var result = await mediator.Send(command);

        return new MessageSampleId { Id = result };
    }

    /// <summary>
    /// Редактирование шаблона сообщения
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MessageSampleId> Edit(MessageSampleModel request,
        ServerCallContext context)
    {
        var command = new EditMessageSampleCommand(request.Title, request.Content, request.Id);
        var result = await mediator.Send(command);

        return new MessageSampleId { Id = result };
    }

    /// <summary>
    /// Удаление шаблона сообщения
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MessageSampleId> Delete(MessageSampleId request,
        ServerCallContext context)
    {
        var command = new DeleteMessageSampleCommand(request.Id);
        var result = await mediator.Send(command);

        return new MessageSampleId { Id = result };
    }

    /// <summary>
    /// Получение информации по шаблону сообщения
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MessageSampleModel> Info(MessageSampleId request, 
        ServerCallContext context)
    {
        var query = new InfoMessageSampleQuery(request.Id);
        var result = await mediator.Send(query);

        return new MessageSampleModel { Id = result.Id, Content = result.Content, Title = result.Title };
    }

    /// <summary>
    /// Получение всего списка шаблонов сообщений
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<AllMessageSampleResponse> All(AllMessageSampleRequest request, ServerCallContext context)
    {
        var query = new AllMessageSampleQuery(request.Count, request.Offset);
        var result = await mediator.Send(query);

        var response = new AllMessageSampleResponse();
        response.Messages.AddRange(result.Select(dto => new MessageSampleModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content
        }));

        return response;
    }
}