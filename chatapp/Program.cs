using chatapp;

class Program
{
    static void Main()
    {
        // Display a main menu
        Console.WriteLine("Chat program 101");
        Console.WriteLine("Choose mode:");
        Console.WriteLine("1. Server");
        Console.WriteLine("2. Client");

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
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
