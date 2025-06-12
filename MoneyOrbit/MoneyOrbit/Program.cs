using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoneyOrbit.Application.Extensions;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Application.Data.Repository; // Assuming this is where UserRepository is
using MoneyOrbit.Core.Entities;
using MoneyOrbit.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Add application-specific services first (from your extension method or here directly)
// This is your existing extension method - it's a good pattern.
builder.Services.AddApplicationServices();

// If AddApplicationServices doesn't register these, you can add them manually:
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IUserRepository<IUser>, UserRepository>();


// 2. Add services required for JWT Authentication
builder.Services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();


// 3. Configure the Authentication middleware
builder.Services.AddAuthentication(options =>
{
    // Set the default scheme to JWT Bearer.
    // This tells the [Authorize] attribute to use JWT validation.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Define how the incoming JWT should be validated.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Validate the key that signed the token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),

        // Validate the issuer (who created the token)
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        // Validate the audience (who the token was intended for)
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        
        // Validate that the token has not expired
        ValidateLifetime = true,

        // You can add a clock skew to allow for small time differences between servers
        ClockSkew = TimeSpan.Zero
    };
});


// 4. Add other standard services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- Build the application ---
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: The order of this middleware is critical.
// 1. UseAuthentication(): It finds and validates the token, creating the user principal (HttpContext.User).
// 2. UseAuthorization(): It checks if the authenticated user is permitted to access the requested resource.
app.UseAuthentication();
app.UseAuthorization();


// Map controllers to their routes
app.MapControllers();

// Run the application
app.Run();