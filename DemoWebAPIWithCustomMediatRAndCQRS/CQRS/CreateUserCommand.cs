using CustomMediatR.Common.Interfaces;

namespace DemoWebAPIWithCustomMediatRAndCQRS.CQRS;

public class CreateUserCommand(string Name) : IRequest<Guid>;

public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request)
    {
        return await Task.FromResult(Guid.NewGuid());
    }
}