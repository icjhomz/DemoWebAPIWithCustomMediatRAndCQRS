using CustomMediatR.Common.Interfaces;

namespace DemoWebAPIWithCustomMediatRAndCQRS.CQRS;

public class GetUserNameCommand(Guid UserId) : IRequest<string>;

public sealed class GetUserNameHandler : IRequestHandler<GetUserNameCommand, string>
{
    public async Task<string> Handle(GetUserNameCommand request)
    {
        // Simulate retrieving the user's name based on the UserId
        return await Task.FromResult("John Doe");
    }
}
