using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Application.MediatR.Interceptors;

public class ErrorHandlingInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (ValidationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                string.Join(Environment.NewLine, ex.Errors.Select(e => e.ErrorMessage))));
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            throw new RpcException(new Status(StatusCode.NotFound,
                string.Join(Environment.NewLine, ex.Status.Detail)));
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated,
                string.Join(Environment.NewLine, ex.Status.Detail)));
        }
        catch (Exception ex)
        {
            // Можно добавить логирование ошибки, если нужно
            throw new RpcException(new Status(StatusCode.Internal, "Неожиданная ошибка с сервера. " + ex.Message));
        }
    }
}