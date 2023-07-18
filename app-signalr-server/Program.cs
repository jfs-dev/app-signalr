using app_signalr_server.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();

app.MapHub<ChatHub>("/chat");

app.Run();
