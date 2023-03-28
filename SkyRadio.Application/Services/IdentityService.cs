using System;
using Microsoft.AspNetCore.Identity;
using SkyRadio.Application.DTOs.Identity;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Domain.Commons;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenProviderService _tokenProviderService;

    public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenProviderService tokenProviderService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenProviderService = tokenProviderService;
    }

    public async ValueTask<Response<dynamic>> AuthentictionAsync(AuthenticationRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
            throw new ApplicationException($"No Account registred with this username {request.Username}");

        var authResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        if (!authResult.Succeeded)
        {
            if (authResult.IsLockedOut) throw new ApplicationException("Account loked out");

            throw new ApplicationException("Invalid credentials");
        }
        if (!user.EmailConfirmed) throw new ApplicationException("User's email address not confirmed");


        var token = await _tokenProviderService.GenerateAccessToken(user);
        dynamic response = new ExpandoObject();

        response.UserId = user?.Id;
        response.Username = user?.UserName;
        response.Email = user?.Email;
        response.Verified = user?.EmailConfirmed;
        response.AuthenticationToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new Response<dynamic>(response, "Authenticated");
    }

    public async ValueTask<Response<dynamic>> RegisterAsync(RegistrationRequest request)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        var userWithSameUsername = await _userManager.FindByNameAsync(request.Username);

        if (userWithSameEmail != null) throw new ApplicationException("Email address already taken");
        if (userWithSameUsername != null) throw new ApplicationException("Username already taken");

        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email,
            EmailConfirmed = true
        };

        var registerResult = await _userManager.CreateAsync(user, request.Password);
        if (!registerResult.Succeeded)
        {
            StringBuilder @string = new StringBuilder();
            foreach (var error in registerResult.Errors) @string.AppendLine(error.Description);

            throw new ApplicationException("Failed to register " + @string);
        }

        return new Response<dynamic>(isSucceed:true,"Account created successfully.");
    }
}
