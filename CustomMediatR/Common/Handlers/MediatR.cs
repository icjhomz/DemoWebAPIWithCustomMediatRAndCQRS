using CustomMediatR.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMediatR.Common.Handlers;

public class MediatR : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public MediatR(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
    {
        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var behaviors = _serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>().ToList();

        RequestHandlerDelegate<TResponse> handlerDelegate = () => handler.Handle(request);

        foreach (var behavior in behaviors.AsEnumerable().Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.Handle(request, next, default);
        }

        return await handlerDelegate();
    }
}
