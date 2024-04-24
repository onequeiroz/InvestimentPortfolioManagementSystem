using InvestimentPortfolioManagementSystem.API.Config;
using InvestimentPortfolioManagementSystem.API.Services;
using InvestimentPortfolioManagementSystem.Application.Context;
using Microsoft.EntityFrameworkCore;
using Quartz;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ResolveDependencies();

// Email Worker
builder.Services.AddSendGrid(options =>
    options.ApiKey = builder.Configuration.GetValue<string>("SendGridApiKey"));
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.ScheduleJob<SendMailService>(trigger => trigger
        .WithIdentity("SendRecurringMailTrigger")
        .WithSimpleSchedule(s =>
            s.WithIntervalInMinutes(60) // TODO: Mudar para 1 dia antes de entregar o projeto
                    //.RepeatForever() TODO: descomentar
        )
        .WithDescription("This trigger will run every 15 seconds to send emails.")
    );
});

builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
