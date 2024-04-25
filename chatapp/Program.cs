using chatapp;

class Program
{
    static void Main()
    {
        // Display a main menu
        Console.WriteLine("Chat program");
        Console.WriteLine("Choose mode:");
        Console.WriteLine("1. Server");
        Console.WriteLine("2. Client");
        Console.WriteLine("3. Exit");

        while (true)
        {
            switch (Console.ReadLine())
            {
                case "1":
                    Server.Start();
                    break;
                case "2":
                    Client.Start();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
