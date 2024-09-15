using Hangfire;
using RentCarApi.API;
using RentCarApi.API.Extensions;
using RentCarApi.Application;
using RentCarApi.Application.Common.Interfaces.BackgroundJob;
using RentCarApi.Infrastructure;
using RentCarApi.Persistence;
using RentCarApi.SignalR;
using Serilog;
using TimeZoneConverter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddWebApiDI(builder.Host);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSignalRServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRCorsPolicy", builder =>
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentCar API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "RentCar API V2");
    });

}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseCors("SignalRCorsPolicy");
app.UseAuthorization();
app.UseHangfireDashboard();
app.UseHangfireServer();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var rentalService = services.GetRequiredService<IRentReminderService>();
    TimeZoneInfo azerbaijanTimeZone = TZConvert.GetTimeZoneInfo("Asia/Baku");

    RecurringJob.AddOrUpdate(
    "send-reminder-emails",
    () => rentalService.SendEndOfRentalEmails(),
    Cron.Daily(15, 38),
    timeZone: azerbaijanTimeZone);
}

app.MapHubs();
using (var scope = app.Services.CreateScope())
{
    await scope.CreateRoleAsync();
}
//app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();
app.Run();