using HostelManagers.IAM.Application.Internal.CommandServices;
using HostelManagers.IAM.Application.Internal.OutboundServices;
using HostelManagers.IAM.Application.Internal.QueryServices;
using HostelManagers.IAM.Domain.Repositories;
using HostelManagers.IAM.Domain.Services;
using HostelManagers.IAM.Infrastructure.Hashing.BCrypt.Services;
using HostelManagers.IAM.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using HostelManagers.IAM.Infrastructure.Tokens.JWT.Configuration;
using HostelManagers.IAM.Infrastructure.Tokens.JWT.Services;
using HostelManagers.IAM.Interfaces.ACL;
using HostelManagers.IAM.Interfaces.ACL.Services;
using HostelManagers.Shared.Domain.Repositories;
using HostelManagers.Shared.Infrastructure.Interfaces.ASP.Configuration;
using HostelManagers.Shared.Infrastructure.Mediator.Cortex.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Configuration;
using HostelManagers.Shared.Infrastructure.Persistence.EFC.Repositories;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using HostelManagers.Hotels.Application.Internal.CommandServices;
using HostelManagers.Hotels.Application.Internal.QueryServices;
using HostelManagers.Hotels.Domain.Repositories;
using HostelManagers.Hotels.Domain.Services;
using HostelManagers.Hotels.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.Reservations.Application.Internal.CommandServices;
using HostelManagers.Reservations.Application.Internal.QueryServices;
using HostelManagers.Reservations.Domain.Repositories;
using HostelManagers.Reservations.Domain.Services;
using HostelManagers.Reservations.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.Rooms.Application.Internal.CommandServices;
using HostelManagers.Rooms.Application.Internal.QueryServices;
using HostelManagers.Rooms.Domain.Repositories;
using HostelManagers.Rooms.Domain.Services;
using HostelManagers.Rooms.Infrastructure.Persistence.EFC.Repositories;
using HostelManagers.Suscriptions.Application.Internal.CommandServices;
using HostelManagers.Suscriptions.Application.Internal.QueryServices;
using HostelManagers.Suscriptions.Domain.Repositories;
using HostelManagers.Suscriptions.Domain.Services;
using HostelManagers.Suscriptions.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Hostel Managers - API",
            Version = "v1",
            Description = "This is a project from HostelManagers Web Applications Course",
            TermsOfService = new Uri("https://github.com/velardesoft"),
            Contact = new OpenApiContact
            {
                Name = "Alls Student Course Project",
                Email = "codydevops@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Hotels Bounded Context
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();

// Rooms Bounded Context
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();

// Suscription Bounded Context
builder.Services.AddScoped<ISuscriptionRepository, SuscriptionRepository>();
builder.Services.AddScoped<ISuscriptionCommandService, SuscriptionCommandService>();
builder.Services.AddScoped<ISuscriptionQueryService, SuscriptionQueryService>();
builder.Services.AddScoped<IPayPalService, PayPalService>();

// Reservation Bounded Context
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandService>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryService>();

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(Program)], configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();