namespace app_signalr_client.Interfaces;

public interface IChatServices
{
    Task SendMessageAsync();
    
    Task SendMessageToUserAsync();

    Task SendMessageToCallerAsync();

    Task SendMessageToGroupAsync();

    Task GetSignalRServerInfosAsync();
}
