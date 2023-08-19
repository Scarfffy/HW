using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    internal static readonly Dictionary<string, ClientHandler> clients = new Dictionary<string, ClientHandler>();
    internal static readonly object clientsLock = new object();

    static void Main(string[] args)
    {
        TcpListener server = null;

        try
        {
            int port = 8888;
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server started on port " + port);

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ClientHandler clientHandler = new ClientHandler(client);
                Thread clientThread = new Thread(clientHandler.HandleClient);
                clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            server?.Stop();
        }
    }

    internal static void AddClient(string username, ClientHandler clientHandler)
    {
        lock (clientsLock)
        {
            clients.Add(username, clientHandler);
        }
    }

    public static void RemoveClient(string username)
    {
        lock (clientsLock)
        {
            clients.Remove(username);
        }
    }

    public static void BroadcastMessage(string message, ClientHandler sender = null)
    {
        lock (clientsLock)
        {
            foreach (var client in clients.Values)
            {
                if (client != sender)
                {
                    client.SendMessage(message);
                }
            }
        }
    }
}

class ClientHandler
{
    private TcpClient client;
    private NetworkStream stream;
    private string username;

    public ClientHandler(TcpClient client)
    {
        this.client = client;
    }

    public void HandleClient()
    {
        stream = client.GetStream();

        while (true)
        {
            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string message = Encoding.ASCII.GetString(data, 0, bytesRead);

            if (message.StartsWith("@"))
            {
                string[] parts = message.Split(' ', 2);
                if (parts.Length >= 2)
                {
                    string recipient = parts[0].Substring(1);
                    string privateMessage = parts[1];
                    SendPrivateMessage(privateMessage, recipient);
                }
                else
                {
                    string errorMessage = "Invalid private message format. Usage: @recipient message";
                    SendMessage(errorMessage);
                }
            }
            else
            {
                Server.BroadcastMessage(username + ": " + message, this);
            }
        }
    }

    private void SendPrivateMessage(string message, string recipient)
    {
        lock (Server.clientsLock)
        {
            if (Server.clients.TryGetValue(recipient, out var targetClient))
            {
                string formattedMessage = $"[Private] {username}: {message}";
                targetClient.SendMessage(formattedMessage);
            }
            else
            {
                string errorMessage = $"User '{recipient}' not found or offline.";
                SendMessage(errorMessage);
            }
        }
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }
}
