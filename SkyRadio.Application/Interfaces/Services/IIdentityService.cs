using System.Threading.Tasks;
using SkyRadio.Application.DTOs.Identity;
using SkyRadio.Domain.Commons;

namespace SkyRadio.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        ValueTask<Response<dynamic>> RegisterAsync(RegistrationRequest request);
        ValueTask<Response<dynamic>> AuthentictionAsync(AuthenticationRequest request);
    }
}
