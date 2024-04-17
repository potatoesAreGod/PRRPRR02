using System.Net.Sockets;
using System.Text;

namespace chatapp
{
    public static class Client
    {
        public static void Start()
        {
            Console.Clear();
            Console.Write("Enter server IP: ");
            string serverIp = Console.ReadLine();

            TcpClient client = new();
            try
            {
                Console.WriteLine("Connecting to server...");
                client.Connect(serverIp, 8888);
                Console.WriteLine("Connected to server.");

                NetworkStream stream = client.GetStream();

                Thread receiveThread = new(() =>
                {
                    while (true)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            Console.WriteLine("The server closed the connection.");
                            break;
                        }
                        string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                        Console.WriteLine(message);
                    }
                });
                receiveThread.Start();

                while (true)
                {
                    Console.Write("> ");
                    string message = Console.ReadLine();
                    if (!string.IsNullOrEmpty(message))
                    {
                        byte[] buffer = Encoding.Unicode.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}