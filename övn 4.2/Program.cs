class Program
{
    static void CreateContact()
    {
        Console.Clear();

        // Ask for contact details
        Console.WriteLine("Enter first name: ");
        string firstName = Console.ReadLine().Trim() ?? "";

        // Make sure a valid name is entered
        while (string.IsNullOrEmpty(firstName))
        {
            Console.Write("A first name is required");
            Console.Write("Enter first name: ");
            firstName = Console.ReadLine().Trim() ?? "";
        }

        Console.WriteLine("Enter last name: ");
        string lastName = Console.ReadLine().Trim() ?? "";

        Console.WriteLine("Enter phone number: ");
        string phone = Console.ReadLine().Trim() ?? "";

        Console.WriteLine("Enter email: ");
        string email = Console.ReadLine().Trim() ?? "";

        Console.Write("Enter file name: ");
        string fileName = Console.ReadLine().Trim() ?? "";

        while (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("You must enter a name");
            Console.Write("Enter file name: ");
            fileName = Console.ReadLine().Trim() ?? "";
        }

        Console.Write("Choose file path (Downloads): ");

        // Stores the path to the desired file location.
        string filePath = Console.ReadLine().Trim() ?? "";
        string defaultDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

        if (string.IsNullOrEmpty(filePath))
            filePath = defaultDir;

        string file = filePath + "\\" + fileName + ".txt";
        string content = firstName + "\n" + lastName + "\n" + phone + "\n" + email;
        WriteFile(file, content);
    }

    static void WriteFile(string file, string content)
    {
        try
        {
            // Ask before overwriting
            if (File.Exists(file))
            {
                Console.Write("File already exists. Do you want to overwrite? [Y/n]: ");
                if (Console.ReadLine() != "Y")
                    return;
            }

            /// Create a FileStream and write content
            FileStream outStream = new(file, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new(outStream);
            writer.Write(content);
            writer.Dispose();
        }
        // Catch exceptions
        catch (FileNotFoundException)
        {
            Console.WriteLine("File {0} was not found", file);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have access to file {0}", file);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("An unexpected error occurred: " + e.Message);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }

    static void ReadContact()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Enter full file path and name: ");
            /// Stores the path to the desired file
            string filePath = Console.ReadLine().Trim() ?? "";

            while (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("You must enter a file");
                Console.Write("Enter file: ");
                filePath = Console.ReadLine().Trim() ?? ""; // Stores file name.
            }

            Console.WriteLine(filePath);
            /// Opens a stream to the file
            FileStream inStream = new(filePath, FileMode.Open, FileAccess.Read);

            /// Translate the binary data into text characters
            StreamReader reader = new(inStream);

            /// Stores all file content
            string firstName = reader.ReadLine() ?? "";
            string lastName = reader.ReadLine() ?? "";
            string phone = reader.ReadLine() ?? "";
            string email = reader.ReadLine() ?? "";

            /// Display text
            Console.Clear();
            Console.WriteLine("Name: {0} {1}", firstName, lastName);
            Console.WriteLine("Phone: {0}", phone);
            Console.WriteLine("Email: {0}", email);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            reader.Dispose();
        }
        // Catch exceptions
        catch (FileNotFoundException)
        {
            Console.WriteLine("File was not found!");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have access to that file");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("An unexpected error occurred: " + e.Message);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }

    static void Main()
    {
        while (true)
        {
            // Create user menu
            Console.Clear();
            Console.WriteLine("1. Create new/edit contact");
            Console.WriteLine("2. Load contact");
            Console.WriteLine("3. Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateContact();
                    break;
                case "2":
                    ReadContact();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Enter a valid option");
                    break;
            }
        }
    }
}

