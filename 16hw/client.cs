using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    private static TcpClient client;
    private static NetworkStream stream;
    private static string username;

    static void SendHistoryRequest()
    {
        SendMessage("/historyrequest");
    }

    static void ReceiveHistory()
    {
        byte[] data = new byte[1024];
        int bytesRead = stream.Read(data, 0, data.Length);
        string history = Encoding.ASCII.GetString(data, 0, bytesRead);
        Console.WriteLine("Message History:");
        Console.WriteLine(history);
    }
    static void FindServers()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8889);

        byte[] data = Encoding.ASCII.GetBytes("ServerSearch");
        udpClient.Send(data, data.Length, endPoint);

        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] responseBytes = udpClient.Receive(ref serverEndPoint);
        string response = Encoding.ASCII.GetString(responseBytes);
        Console.WriteLine("Server found at: " + serverEndPoint.Address);

        udpClient.Close();
    }


    static void Main(string[] args)
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8888);
            stream = client.GetStream();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            stream?.Close();
            client?.Close();
        }

        Console.Write("Enter your username: ");
        username = Console.ReadLine();

        try
        {
            client = new TcpClient("127.0.0.1", 8888);
            stream = client.GetStream();

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() == "exit")
                    break;
                else if (message == "/historyrequest")
                {
                    ReceiveHistory();
                }
                else if (message.ToLower() == "/findservers")
                {
                    FindServers();
                }
                else if (message == "/history")
                {
                    SendHistoryRequest();
                }
                else if (message.StartsWith("@"))
                {
                    SendPrivateMessage(message);
                }
                else
                {
                    SendMessage(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            stream?.Close();
            client?.Close();
        }
    }

    static void ReceiveMessages()
    {
        byte[] data = new byte[1024];
        while (true)
        {
            int bytesRead = stream.Read(data, 0, data.Length);
            string message = Encoding.ASCII.GetString(data, 0, bytesRead);
            Console.WriteLine(message);
        }
    }

    static void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    static void SendPrivateMessage(string message)
    {
        Console.Write("Enter recipient's username: ");
        string recipient = Console.ReadLine();
        string formattedMessage = $"@{recipient} {message}";
        SendMessage(formattedMessage);
    }
}
