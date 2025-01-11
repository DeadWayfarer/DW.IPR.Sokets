using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Указываем IP-адрес и порт для сервера
IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
int port = 8888;

// Создаем TCP-сокет
TcpListener listener = new TcpListener(ipAddress, port);

// Запускаем сервер
listener.Start();
Console.WriteLine("Сервер запущен. Ожидание подключений...");

// Принимаем клиента
TcpClient client = listener.AcceptTcpClient();
Console.WriteLine("Клиент подключен.");

// Получаем поток для чтения и записи данных
NetworkStream stream = client.GetStream();

// Чтение данных от клиента
byte[] buffer = new byte[256];
int bytesRead = stream.Read(buffer, 0, buffer.Length);
string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine("Получено от клиента: " + receivedMessage);

// Отправка данных клиенту
string responseMessage = "Сообщение получено!";
byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
stream.Write(responseData, 0, responseData.Length);
Console.WriteLine("Ответ отправлен клиенту.");

// Закрываем соединение
client.Close();
listener.Stop();

Console.ReadKey(true);