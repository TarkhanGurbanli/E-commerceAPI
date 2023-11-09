using EcommerceApi.Business.Consumer;
using EcommerceApi.Business.DependencyResolvers;
using EcommerceApi.Business.Policy;
using EcommerceApi.Entities.SharedModels;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//DependencyResolvers icerisindeki Servicelerimiz baska claasda yazib burada cagiririq
builder.Services.Run();


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ReceiveEmailConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost");
        cfg.Message<SendEmailCommand>(x => x.SetEntityName("SendEmailCommand"));
        cfg.ReceiveEndpoint("send-email-command", c =>
        {
            c.ConfigureConsumer<ReceiveEmailConsumer>(ctx);
        });
    });
});

// Add services to the container.

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nmDLKAna9f9WEKPPH7z3tgwnQ433FAtrdP5c9AmDnmuJp9rzwTPwJ9yUu")),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();

var optionsOpen = new OpenApiSecurityScheme
{
    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey
};

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", optionsOpen);
});


builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
