using BlazingQuiz.Api.Data;
using BlazingQuiz.Api.Data.Entities;
using BlazingQuiz.Api.Endpoints;
using BlazingQuiz.Api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QuizContext>(options =>
{ 
    var conectionString = builder.Configuration.GetConnectionString("Quiz");
    options.UseSqlServer(conectionString);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var secretKey = builder.Configuration.GetValue<string>("Jwt:Secret");
    var symmetricKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = symmetricKey,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy =>
    {
  
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddTransient<AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    ApplyDbMigrations(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.MapAuthEndpoints();
app.Run();

static void ApplyDbMigrations(IServiceProvider sp)
{
    var scoped = sp.CreateScope();
    var context = scoped.ServiceProvider.GetRequiredService<QuizContext>();
    if(context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

