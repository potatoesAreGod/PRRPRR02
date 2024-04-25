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
            int defaultPort = 8888;
            int port = 0;

            // Create a server object
            TcpListener server = null;
            try
            {
                // Start the server
                Console.Write("Enter port for server ({0}): ", defaultPort);
                string input = Console.ReadLine().Trim();

                // Apply a default of 8888 if user specifed port is invalid
                if (int.TryParse(input, out int userPort))
                {
                    if (userPort > 65535 || userPort < 0)
                    {
                        Console.WriteLine("Invalid port. Applying default of {0}", defaultPort);
                        port = defaultPort;
                    } else
                    {
                        port = userPort;
                    }
                } else
                {
                    Console.WriteLine("Invalid port. Applying default of {0}", defaultPort);
                    port = defaultPort;
                }

                // Start the server on specified port
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                Console.WriteLine("Server is running. Listening on port {0}", port);

                while (true)
                {
                    // Accept incoming clients
                    TcpClient client = server.AcceptTcpClient();

                    // Add client to list
                    clients.Add(client);

                    // Create a new thread for each client
                    Thread clientThread = new(HandleClient);
                    clientThread.Start(client);
                }
            }
            // Handle exceptions
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                // Make sure to stop the server when we are done
                server.Stop();
            }
        }

        private static void HandleClient(object obj)
        {
            // Create a client object for storing the client in
            TcpClient client = (TcpClient)obj;

            // Store client IP
            string clientEndPoint = client.Client.RemoteEndPoint.ToString();

            NetworkStream stream = client.GetStream();

            Console.WriteLine("Connected: {0}", clientEndPoint);

            while (true)
            {
                try
                {
                    // Read incoming data
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // If we don't recieve new data the connection must be dead
                    if (bytesRead == 0)
                    {
                        Console.WriteLine("Disconnected: {0}", clientEndPoint);
                        clients.Remove(client);
                        break;
                    }

                    // Read message and write to console
                    string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("{0}: {1}", clientEndPoint, message);

                    // Send to other clients
                    BroadcastMessage(clientEndPoint, message);
                }

                // Catch exceptions
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                    break;
                }
                }
            // Close the connection when we are done
            client.Close();
        }

        // Sends message recived from one client to the other ones
        private static void BroadcastMessage(string sender, string message)
        {
            // Loop though all clients
            foreach (TcpClient client in clients)
            {
                // Read the incoming message
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.Unicode.GetBytes($"{sender}: {message}");

                // Send it to all clients
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
