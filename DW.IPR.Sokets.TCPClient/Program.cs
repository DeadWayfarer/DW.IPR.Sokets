using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Указываем IP-адрес и порт сервера
string serverIp = "127.0.0.1";
int port = 8888;

// Даем команду на подключение
Console.WriteLine("Нажмите любую кнопку чтобы подключиться к серверу...");
Console.ReadKey();

// Создаем TCP-сокет и подключаемся к серверу
TcpClient client = new TcpClient(serverIp, port);
Console.WriteLine("Подключено к серверу.");

// Получаем поток для чтения и записи данных
NetworkStream stream = client.GetStream();

// Отправка данных серверу
string message = "Привет, сервер!";
Console.WriteLine("Нажмите любую кнопку чтобы отправить сообщение серверу...");
Console.ReadKey();

byte[] data = Encoding.UTF8.GetBytes(message);
stream.Write(data, 0, data.Length);
Console.WriteLine("Сообщение отправлено серверу.");

// Чтение ответа от сервера
byte[] buffer = new byte[256];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine("Получено от сервера: " + receivedMessage);

// Закрываем соединение
client.Close();

Console.ReadKey();