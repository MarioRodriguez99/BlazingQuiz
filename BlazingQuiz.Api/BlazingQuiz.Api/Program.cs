using BlazingQuiz.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<QuizContext>(options =>{ var conectionString = builder.Configuration.GetConnectionString("Quiz");  options.UseSqlServer(conectionString);});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    ApplyDbMigrations(app.Services);
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
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

