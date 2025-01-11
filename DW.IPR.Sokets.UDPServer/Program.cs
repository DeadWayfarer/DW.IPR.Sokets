using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

UdpClient udpServer = new UdpClient(8888);
IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

while (true)
{
    byte[] receivedData = udpServer.Receive(ref clientEndPoint);
    string receivedMessage = Encoding.UTF8.GetString(receivedData);
    Console.WriteLine("Получено от клиента: " + receivedMessage);

    string responseMessage = "Сообщение получено!";
    byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
    udpServer.Send(responseData, responseData.Length, clientEndPoint);
}