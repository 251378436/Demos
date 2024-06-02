using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FirstAPI.Inbound;

public static class InboundExtentions
{
    public static IServiceCollection RegisterInbound(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.MetadataAddress = configuration["inboundAuth:jwtBearer:metadataAddress"]!;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5), // only for demo, please check your own project settings
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["inboundAuth:jwtBearer:audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["inboundAuth:jwtBearer:issuer"],
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

        return services;
    }
}