using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    private static TcpClient client;
    private static NetworkStream stream;
    private static string username;

    static void Main(string[] args)
    {
        try
        {
            client = new TcpClient("127.0.0.1", 8888);
            stream = client.GetStream();

            // ... інший код ...
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

                if (message.StartsWith("@"))
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
