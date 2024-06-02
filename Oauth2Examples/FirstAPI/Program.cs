using FirstAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OutboundAuth>(builder.Configuration.GetSection("outboundAuth:auth0_group_tenant"));

builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.MetadataAddress = builder.Configuration["inboundAuth:jwtBearer:metadataAddress"]!;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["inboundAuth:jwtBearer:audience"],
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["inboundAuth:jwtBearer:issuer"],
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

builder.Services.AddTransient<Auth0HttpHandler>();

builder.Services.AddHttpClient("secondapi", (provider, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5196/");
}).AddHttpMessageHandler<Auth0HttpHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();