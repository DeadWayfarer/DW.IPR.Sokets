using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Нажмите любую кнопку чтобы подключиться к серверу...");
Console.ReadKey(true);

// Указываем адрес WebSocket-сервера
Uri serverUri = new Uri("ws://localhost:5000/ws/");

// Создаем WebSocket-клиент
using ClientWebSocket webSocket = new ClientWebSocket();

try
{
    // Подключаемся к серверу
    Console.WriteLine("Подключение к серверу...");
    await webSocket.ConnectAsync(serverUri, CancellationToken.None);
    Console.WriteLine("Подключение установлено.");

    // Отправляем сообщение серверу
    string message = "Привет, сервер!";
    Console.WriteLine("Нажмите любую кнопку чтобы отправить сообщение серверу...");
    Console.ReadKey(true);

    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    Console.WriteLine("Сообщение отправлено: " + message);

    // Получаем ответ от сервера
    byte[] buffer = new byte[256];
    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
    Console.WriteLine("Получено от сервера: " + receivedMessage);

    // Закрываем соединение
    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Закрытие соединения", CancellationToken.None);
    Console.WriteLine("Соединение закрыто.");
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
}

Console.ReadKey(true);