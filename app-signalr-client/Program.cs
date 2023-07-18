using app_signalr_client.Services;

ChatServices chatServices = new();

ConsoleKeyInfo input;

do
{
    Console.WriteLine("Chat Menu");
    Console.WriteLine("---------");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("\n[A] ");
    Console.ResetColor();
    Console.WriteLine("Enviar mensagens para todos os usuários conectados no servidor");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("[B] ");
    Console.ResetColor();
    Console.WriteLine("Enviar mensagens privadas para um usuário específico conectado no servidor");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("[C] ");
    Console.ResetColor();
    Console.WriteLine("Enviar mensagens de volta para o usuário emitente conectado no servidor");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("[D] ");
    Console.ResetColor();
    Console.WriteLine("Enviar mensagens para um grupo de usuários conectados no servidor");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("[E] ");
    Console.ResetColor();
    Console.WriteLine("Obter informações do servidor");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("\n[Esc] ");
    Console.ResetColor();
    Console.WriteLine("para sair");
    Console.WriteLine();

    input = Console.ReadKey();

    switch (input.Key)
    {
        case ConsoleKey.A:
            Console.WriteLine();
            await chatServices.SendMessageAsync();
            Console.WriteLine();
            break;

        case ConsoleKey.B:
            Console.WriteLine();
            await chatServices.SendMessageToUserAsync();
            Console.WriteLine();
            break;

        case ConsoleKey.C:
            Console.WriteLine();
            await chatServices.SendMessageToCallerAsync();
            Console.WriteLine();
            break;

        case ConsoleKey.D:
            Console.WriteLine();
            await chatServices.SendMessageToGroupAsync();
            Console.WriteLine();
            break;

        case ConsoleKey.E:
            Console.WriteLine();
            await chatServices.GetSignalRServerInfosAsync();
            Console.WriteLine();
            break;

        case ConsoleKey.Escape:
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nOpção inválida.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n[Esc] ");
            Console.ResetColor();
            Console.WriteLine("para voltar ao menu\n");
            Console.ReadKey(true);
            break;
    }
} while (input.Key != ConsoleKey.Escape);
