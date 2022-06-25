using MediatR;
using System.Text;
using MassTransit;
using MessageService.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;

var loggerConfiguration = new LoggerConfiguration();
Log.Logger = loggerConfiguration
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithThreadId()
    .Enrich.WithThreadName()
    .WriteTo.Console(theme: SystemConsoleTheme.Literate)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Logging.ClearProviders();
builder.Host.UseSerilog(Log.Logger);
//builder.Logging.AddSerilog(Log.Logger);


builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Message Api", Version = "v1" });
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
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = configuration["Token:Issuer"],
                     ValidAudience = configuration["Token:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<MessageConsumer>();
    x.AddConsumer<ActivityConsumer>();
    x.AddConsumer<BlockUserConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["RabbitmqSettings:Host"], Convert.ToUInt16(configuration["RabbitmqSettings:Port"]), "/", h =>
                    {
                        h.Username(configuration["RabbitmqSettings:Username"]);
                        h.Password(configuration["RabbitmqSettings:Password"]);
                    });
        cfg.ConfigureEndpoints(context);
    });

    x.AddRequestClient<Message>(new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.Consumer<MessageConsumer>()}"));
    x.AddRequestClient<Activity>(new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.Consumer<ActivityConsumer>()}"));
    x.AddRequestClient<BlockUser>(new Uri($"queue:{KebabCaseEndpointNameFormatter.Instance.Consumer<BlockUserConsumer>()}"));
});

builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.Run();