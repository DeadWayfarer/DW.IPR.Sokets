using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Создаем http клиент
using HttpListener httpListener = new HttpListener();
httpListener.Prefixes.Add("http://localhost:5000/ws/");
httpListener.Start();
Console.WriteLine("Сервер запущен. Ожидание подключений...");

HttpListenerContext context = await httpListener.GetContextAsync();
if (context.Request.IsWebSocketRequest)
{
    // Берем из запроса web сокет
    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
    WebSocket webSocket = webSocketContext.WebSocket;

    // Получает от клиента сообщение
    byte[] buffer = new byte[256];
    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
    Console.WriteLine("Получено от клиента: " + receivedMessage);

    // Отправляем сообщение клиенту
    string responseMessage = "Сообщение получено!";
    byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
    await webSocket.SendAsync(new ArraySegment<byte>(responseData), WebSocketMessageType.Text, true, CancellationToken.None);

    // Закрываем подключение предупреждая клиента о закрытии
    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
}
httpListener.Stop();

Console.ReadKey(true);