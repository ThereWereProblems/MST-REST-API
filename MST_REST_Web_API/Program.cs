using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MST_REST_Web_API;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Middleware;
using MST_REST_Web_API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssurl,
        ValidAudience = authenticationSettings.JwtIssurl,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddControllers(); 
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCennection")
    ));
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddScoped < IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddCors(options => { // <--- define a policy
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins(
            "http://localhost:3001"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

//seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();

app.UseHttpsRedirection();
SeedDatabase();


app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();

void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Seed();
    }
}
