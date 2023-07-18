using Microsoft.AspNetCore.SignalR;

namespace app_signalr_server.Hubs;

public class ChatHub : Hub
{
    private static readonly Dictionary<string, string> userConnections = new();
    private static readonly Dictionary<string, List<string>> groupConnections = new();

    public override async Task OnConnectedAsync()
        => await base.OnConnectedAsync();

    public override async Task OnDisconnectedAsync(Exception? exception)
        => await base.OnDisconnectedAsync(exception);

    public void AddUser(string userName)
    {
        string connectionId = Context.ConnectionId;

        userConnections[connectionId] = userName;
    }

    public void RemoveUser()
    {
        string connectionId = Context.ConnectionId;

        userConnections.Remove(connectionId);
    }

    public List<string> GetConnectedUsers()
        => new(userConnections.Values);

    public async Task AddGroupAsync(string groupName)
    {
        string connectionId = Context.ConnectionId;

        if (!groupConnections.ContainsKey(groupName)) groupConnections[groupName] = new();
        groupConnections[groupName].Add(connectionId);

        await Groups.AddToGroupAsync(connectionId, groupName);
    }

    public async Task RemoveGroupAsync(string groupName)
    {
        string connectionId = Context.ConnectionId;

        groupConnections[groupName].Remove(connectionId);
        if (groupConnections[groupName].Count == 0) groupConnections.Remove(groupName);

        await Groups.RemoveFromGroupAsync(connectionId, groupName);
    }

    public List<string> GetGroups()
        => new(groupConnections.Keys);

    public List<string> GetGroupUsers(string groupName)
    {
        List<string> groupUsers = new();
        
        if (groupConnections.TryGetValue(groupName, out List<string>? value))
            foreach (string user in value)
                groupUsers.Add(userConnections.FirstOrDefault(x => x.Key == user).Value);

        return groupUsers;
    }

    public async Task SendMessageAsync(string user, string message)
        => await Clients.All.SendAsync("ReceiveMessage", user, message);

    public async Task SendMessageToUserAsync(string recipient, string user, string message)
    {
        var recipientConnectionId = userConnections.FirstOrDefault(x => x.Value == recipient).Key;

        if (!string.IsNullOrEmpty(recipientConnectionId))
        {
            await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveMessage", "Servidor SignalR", $"Mensagem entregue ao destinatário '{recipient}'.");
        }
        else
            await Clients.Caller.SendAsync("ReceiveMessage", "Servidor SignalR", $"Destinatário '{recipient}' não disponível ou não existe.");
    }

    public async Task SendMessageToCallerAsync(string user, string message)
        => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

    public async Task SendMessageToGroupAsync(string groupName, string user, string message)
        => await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
}
