// ---- REQUIRED USING STATEMENTS ----
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // <-- ADD THIS for Swagger security definitions
using MoneyOrbit.Application.Extensions;
using MoneyOrbit.Application.Interfaces.IApplication.IHelper;
using MoneyOrbit.Application.Interfaces.IServices;
using System.Security.Claims; // <-- ADD THIS for ClaimTypes.Role
using System.Text;

// ---- This section assumes your custom services are registered in these namespaces ----
// using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
// using MoneyOrbit.Application.Interfaces.IEntities;
// using MoneyOrbit.Application.Data.Repository; 
// using MoneyOrbit.Core.Entities;
// using MoneyOrbit.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// 1. Add application-specific services (your existing extension method)
builder.Services.AddApplicationServices();


// 2. Configure the Authentication middleware to validate incoming JWTs
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,

        // This line is crucial for [Authorize(Roles = "...")] to work correctly.
        // It tells the validator which claim in the token represents the user's role.
        RoleClaimType = ClaimTypes.Role,

        ClockSkew = TimeSpan.Zero
    };
});

// 4. Add Authorization services
builder.Services.AddAuthorization();

// 5. Add other standard services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// ------------------- SWAGGER CONFIGURATION (UPDATED) -------------------
// This is the updated section that adds the "Authorize" button to Swagger.
builder.Services.AddSwaggerGen(options =>
{
    // Set the title and version of your API
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MoneyOrbit API", Version = "v1" });

    // Define the JWT Bearer security scheme
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Use Http for Bearer
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Make Swagger apply the security scheme to all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
// ----------------------- END OF SWAGGER CONFIGURATION -----------------------


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
// 1. UseAuthentication(): It finds and validates the token, creating HttpContext.User.
app.UseAuthentication();
// 2. UseAuthorization(): It checks if the authenticated user is permitted to access the resource.
app.UseAuthorization();

// Map controllers to their routes
app.MapControllers();

// Run the application
app.Run();