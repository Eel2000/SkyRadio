using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkyRadio.Application.DTOs.Identity;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;

namespace SkyRadio.Application.Features.Identity.Commands;

public class AuthenticationCommand : IRequest<Response<object>>
{
    public AuthenticationRequest Authentication { get; set; }
}

public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, Response<object>>
{
    private readonly IIdentityService _identityService;

    public AuthenticationCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Response<object>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.AuthentictionAsync(request.Authentication);
    }
}
