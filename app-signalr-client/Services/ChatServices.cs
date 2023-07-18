using Microsoft.AspNetCore.SignalR.Client;
using app_signalr_client.Interfaces;

namespace app_signalr_client.Services;

public class ChatServices : IChatServices
{
    public const string UrlChat = "http://localhost:5016/chat";

    public async Task SendMessageAsync()
    {
        var chatState = string.Empty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite seu nome: ");
        Console.ResetColor();
        
        var user = Console.ReadLine();

        if (string.IsNullOrEmpty(user)) throw new InvalidOperationException("Favor informar um usuário para conectar-se no servidor!");

        var connection = new HubConnectionBuilder()
            .WithUrl(UrlChat)
            .WithAutomaticReconnect()
            .Build();

        connection.On<string, string>("ReceiveMessage", (userName, message) =>
        {
            if (!string.IsNullOrEmpty(chatState) && user != userName) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n{userName} diz: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{message}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n[Enter] ");
            Console.ResetColor();
            Console.Write("para continuar ou ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[Esc] ");
            Console.ResetColor();
            Console.WriteLine("para voltar ao menu");

            if (!string.IsNullOrEmpty(chatState))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(chatState);
                Console.ResetColor();
            }
        });

        await connection.StartAsync();
        await connection.InvokeAsync("AddUser", user);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} conectado no servidor.");
        Console.ResetColor();

        do
        {
            chatState = "\nDigite sua mensagem: ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(chatState);
            Console.ResetColor();
            
            var message = Console.ReadLine();
            chatState = string.Empty;

            await connection.InvokeAsync("SendMessageAsync", user, message);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        await connection.InvokeAsync("RemoveUser");
        await connection.StopAsync();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} desconectado do servidor.");
        Console.ResetColor();
    }

    public async Task SendMessageToUserAsync()
    {
        var chatState = string.Empty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite seu nome: ");
        Console.ResetColor();
        
        var user = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite o nome do destinatário: ");
        Console.ResetColor();

        var recipient = Console.ReadLine();

        if (string.IsNullOrEmpty(user)) throw new InvalidOperationException("Favor informar um usuário para conectar-se no servidor!");
        if (string.IsNullOrEmpty(recipient)) throw new InvalidOperationException("Favor informar um destinatário conectado no servidor para enviar mensagens!");

        var connection = new HubConnectionBuilder()
            .WithUrl(UrlChat)
            .WithAutomaticReconnect()
            .Build();

        connection.On<string, string>("ReceiveMessage", (userName, message) =>
        {
            if (!string.IsNullOrEmpty(chatState)) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n{userName} diz: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{message}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n[Enter] ");
            Console.ResetColor();
            Console.Write("para continuar ou ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[Esc] ");
            Console.ResetColor();
            Console.WriteLine("para voltar ao menu");

            if (!string.IsNullOrEmpty(chatState))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(chatState);
                Console.ResetColor();
            }
        });

        await connection.StartAsync();
        await connection.InvokeAsync("AddUser", user);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} conectado no servidor.");
        Console.ResetColor();

        do
        {
            chatState = "\nDigite sua mensagem: ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(chatState);
            Console.ResetColor();

            var message = Console.ReadLine();
            chatState = string.Empty;

            await connection.InvokeAsync("SendMessageToUserAsync", recipient, user, message);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} desconectado do servidor.");
        Console.ResetColor();
    }

    public async Task SendMessageToCallerAsync()
    {
        var chatState = string.Empty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite seu nome: ");
        Console.ResetColor();
        
        var user = Console.ReadLine();

        if (string.IsNullOrEmpty(user)) throw new InvalidOperationException("Favor informar um usuário para conectar-se no servidor!");

        var connection = new HubConnectionBuilder()
            .WithUrl(UrlChat)
            .WithAutomaticReconnect()
            .Build();

        connection.On<string, string>("ReceiveMessage", (userName, message) =>
        {
            if (!string.IsNullOrEmpty(chatState) && user != userName) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n{userName} diz: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{message}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n[Enter] ");
            Console.ResetColor();
            Console.Write("para continuar ou ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[Esc] ");
            Console.ResetColor();
            Console.WriteLine("para voltar ao menu");

            if (!string.IsNullOrEmpty(chatState))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(chatState);
                Console.ResetColor();
            }
        });

        await connection.StartAsync();
        await connection.InvokeAsync("AddUser", user);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} conectado no servidor.");
        Console.ResetColor();

        do
        {
            chatState = "\nDigite sua mensagem: ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(chatState);
            Console.ResetColor();
            
            var message = Console.ReadLine();
            chatState = string.Empty;

            await connection.InvokeAsync("SendMessageToCallerAsync", user, message);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        await connection.InvokeAsync("RemoveUser");
        await connection.StopAsync();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} desconectado do servidor.");
        Console.ResetColor();
    }

    public async Task SendMessageToGroupAsync()
    {
        var chatState = string.Empty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite seu nome: ");
        Console.ResetColor();
        
        var user = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("\nDigite o nome do grupo de usuários: ");
        Console.ResetColor();

        var groupName = Console.ReadLine();

        if (string.IsNullOrEmpty(user)) throw new InvalidOperationException("Favor informar um usuário para conectar-se no servidor!");
        if (string.IsNullOrEmpty(groupName)) throw new InvalidOperationException("Favor informar um grupo de usuários para enviar mensagens!");

        var connection = new HubConnectionBuilder()
            .WithUrl(UrlChat)
            .WithAutomaticReconnect()
            .Build();

        connection.On<string, string>("ReceiveMessage", (userName, message) =>
        {
            if (!string.IsNullOrEmpty(chatState) && user != userName) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n{userName} diz: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{message}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n[Enter] ");
            Console.ResetColor();
            Console.Write("para continuar ou ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[Esc] ");
            Console.ResetColor();
            Console.WriteLine("para voltar ao menu");

            if (!string.IsNullOrEmpty(chatState))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(chatState);
                Console.ResetColor();
            }
        });

        await connection.StartAsync();
        await connection.InvokeAsync("AddUser", user);
        await connection.InvokeAsync("AddGroupAsync", groupName);

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} conectado no servidor.");
        Console.ResetColor();

        do
        {
            chatState = "\nDigite sua mensagem: ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(chatState);
            Console.ResetColor();
            
            var message = Console.ReadLine();
            chatState = string.Empty;

            await connection.InvokeAsync("SendMessageToGroupAsync", groupName, user, message);
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        await connection.InvokeAsync("RemoveGroupAsync", groupName);
        await connection.InvokeAsync("RemoveUser");
        await connection.StopAsync();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n{user} desconectado do servidor.");
        Console.ResetColor();
    }

    public async Task GetSignalRServerInfosAsync()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(UrlChat)
            .WithAutomaticReconnect()
            .Build();

        await connection.StartAsync();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nConexão estabelecida com o servidor.");
        Console.ResetColor();

        var connectedUsers = await connection.InvokeAsync<List<string>>("GetConnectedUsers");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nUsuários conectados no servidor:");
        Console.WriteLine("--------------------------------");
        Console.ResetColor();

        foreach (var connectedUser in connectedUsers)
            Console.WriteLine(connectedUser);

        var groups = await connection.InvokeAsync<List<string>>("GetGroups");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nGrupos de usuários no servidor:");
        Console.WriteLine("-------------------------------");
        Console.ResetColor();

        foreach (var group in groups)
        {
            Console.WriteLine(group);

            var groupUsers = await connection.InvokeAsync<List<string>>("GetGroupUsers", group);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nUsuários no grupo {group}:");
            Console.WriteLine("-------------------" + new string('-', group.Length));
            Console.ResetColor();

            foreach (var groupUser in groupUsers)
                Console.WriteLine(groupUser);
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n[Esc] ");
        Console.ResetColor();
        Console.WriteLine("para voltar ao menu");
        Console.ReadKey(true);

        await connection.StopAsync();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nConexão encerrada com o servidor.");
        Console.ResetColor();
    }
}
