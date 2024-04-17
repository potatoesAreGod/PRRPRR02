using System.Net;
using System.Net.Sockets;
using System.Text;

namespace chatapp
{
    public static class Server
    {
        // List to store clients in
        private static readonly List<TcpClient> clients = [];

        public static void Start()
        {
            Console.Clear();
            TcpListener server = null;
            try
            {
                // Start a server
                Console.Write("Enter port for server (8888): ");

                //string inputPath = Console.ReadLine().Trim();
                //string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultSavePath : inputPath;



                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();

                Console.WriteLine("Server is running. Listening on port 8888");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    clients.Add(client);

                    Thread clientThread = new(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                server.Stop();
            }
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;

            string clientEndPoint = client.Client.RemoteEndPoint.ToString();

            NetworkStream stream = client.GetStream();

            Console.WriteLine("Connected: {0}", clientEndPoint);

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Disconnected: {0}", clientEndPoint);
                        clients.Remove(client);
                        break;
                    }

                    string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("{0}: {1}", clientEndPoint, message);

                    BroadcastMessage(clientEndPoint, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                    break;
                }
            }
            client.Close();
        }

        private static void BroadcastMessage(string sender, string message)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.Unicode.GetBytes($"{sender}: {message}");
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
