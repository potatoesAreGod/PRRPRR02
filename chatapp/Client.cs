using System.Net.Sockets;
using System.Text;

namespace chatapp
{
    public static class Client
    {
        public static void Start()
        {
            Console.Clear();
            int defaultPort = 8888;
            int port = 0;

            // Ask for server ip
            Console.Write("Enter server IP: ");
            string serverIp = Console.ReadLine().Trim();
            while (string.IsNullOrWhiteSpace(serverIp))
            {
                Console.WriteLine("Please enter a valid address");
                Console.Write("Enter server IP: ");
                serverIp = Console.ReadLine().Trim();
            }

            // Ask for server port
            Console.Write("Enter port for server ({0}): ", defaultPort);
            string input = Console.ReadLine().Trim();

            // Apply a default of 8888 if user specifed port is invalid
            if (int.TryParse(input, out int userPort))
            {
                if (userPort > 65535 || userPort < 0)
                {
                    Console.WriteLine("Invalid port. Applying default of {0}", defaultPort);
                    port = defaultPort;
                }
                else
                {
                    port = userPort;
                }
            }
            else
            {
                Console.WriteLine("Invalid port. Applying default of {0}", defaultPort);
                port = defaultPort;
            }

            // Create a client object for creating TCP connections
            TcpClient client = new();
            try
            {
                Console.WriteLine("Connecting to {0}:{1}...", serverIp, port);

                // Try connecting to the server
                client.Connect(serverIp, port);
                Console.WriteLine("Connected to {0}:{1}", serverIp, port);

                // Opens a stream for sending/reading data from the server
                NetworkStream stream = client.GetStream();

                // Create a new thread so we can read/write at the same time
                Thread receiveThread = new(() =>
                {
                    while (true)
                    {
                        // Max size for recived messages
                        byte[] buffer = new byte[1024];

                        // Read data from server
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        // If we don't recieve any data the connection must be dead
                        if (bytesRead == 0)
                        {
                            Console.WriteLine("The server closed the connection.");
                            break;
                        }

                        // Convert recived data to human readable text and write to console
                        string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                        Console.WriteLine(message);
                    }
                });
                // Start the thread
                receiveThread.Start();

                while (true)
                {
                    string message = Console.ReadLine();

                    // Ensure we have a message to send and
                    // ensure length is less than 256 to avoid overflows
                    if (!string.IsNullOrEmpty(message) && message.Length <= 256)
                    {
                        // Convert message to bytecode and send
                        byte[] buffer = Encoding.Unicode.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);
                    } else if (message.Length > 256)
                    {
                        Console.WriteLine("Error: Message is too long");
                    }
                }
            }
            // Catch exceptions
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            // Close TCP client before exiting
            finally
            {
                client.Close();
            }
        }
    }
}