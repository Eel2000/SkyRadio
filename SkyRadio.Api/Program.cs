using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SkyRadio.Api.Hubs;
using SkyRadio.Application;
using SkyRadio.Application.Models;
using SkyRadio.Domain.Commons;
using SkyRadio.Persistence;
using System.Text;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSignalR();
    builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());

    var jwtParamSection = builder.Configuration.GetSection("JwtConfiguration");
    var jwtParams = jwtParamSection.Get<JWTParameters>();

    builder.Services.AddScoped(factory => jwtParams);

    builder.Services.Configure<JWTParameters>(builder.Configuration.GetSection("JwtConfiguration"));

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequiredLength = 4;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.RequireHttpsMetadata = false;
        jwtOptions.SaveToken = false;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration[$"JwtConfiguration:Issuer"],
            ValidAudience = builder.Configuration[$"JwtConfiguration:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTParameters:Key"]))
        };

        jwtOptions.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var result =
                    JsonConvert.SerializeObject(new Response<string>("Authentification has failed, please try with correct credentials."));

                return context.Response.WriteAsync(result);
            },
            OnChallenge = context =>
            {
                context.HandleResponse();

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                }

                var result =
                    JsonConvert.SerializeObject(new Response<string>("Not authorized to access this ressource."));

                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";

                var result =
                    JsonConvert.SerializeObject(new Response<string>("Not authorized to access this ressource."));

                return context.Response.WriteAsync(result);
            }
        };

    });

    builder.Services.AddSwaggerGen(opt =>
    {
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });

    builder.Services.AddAuthorization();

    builder.Services.AddApplicationLayer(builder.Configuration);




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseAuthentication();

    app.MapHub<SkyRadioLiveHub>("radio/live");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    //TODO: use serilog to log
}
finally
{
    //TODO: flush.
}
