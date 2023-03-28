using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkyRadio.Application.DTOs.Identity;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;

namespace SkyRadio.Application.Features.Identity.Commands;

public class RegistrationCommand : IRequest<Response<object>>
{
    public RegistrationRequest RegistrationRequest { get; set; }
}

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Response<object>>
{
    private readonly IIdentityService _identityService;

    public RegistrationCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Response<object>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterAsync(request.RegistrationRequest);
        return result;
    }
}
