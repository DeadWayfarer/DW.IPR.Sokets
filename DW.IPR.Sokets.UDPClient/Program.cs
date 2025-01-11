using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Даем команду на подключение
Console.WriteLine("Нажмите любую кнопку чтобы подключиться к серверу...");
Console.ReadKey();

// Отправка данных серверу
UdpClient udpClient = new UdpClient();
IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
string message = "Привет, сервер!";
Console.WriteLine("Нажмите любую кнопку чтобы отправить сообщение серверу...");
Console.ReadKey();
byte[] data = Encoding.UTF8.GetBytes(message);
udpClient.Send(data, data.Length, serverEndPoint);

// Получаем ответ от сервера
byte[] receivedData = udpClient.Receive(ref serverEndPoint);
string receivedMessage = Encoding.UTF8.GetString(receivedData);
Console.WriteLine("Получено от сервера: " + receivedMessage);