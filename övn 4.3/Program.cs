class Contact(string FirstName, string LastName, string Email, string Phone)
{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string Email { get; set; } = Email;
    public string Phone { get; set; } = Phone;
}

class ContactManager
{
    private readonly List<Contact> contacts;
    public ContactManager() { contacts = []; }
    // Default contact saves to user's downloads folder
    readonly string defaultSavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "contacts.txt");

    private bool AreContactsStored()
    {
        // Do not continue if no contacts are saved
        if (contacts.Count == 0)
        {
            Console.WriteLine("No saved contacts");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
            return false;
        }
        return true;
    }

    public void AddContact()
    {
        Console.Clear();

        // Ask for first name
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine().Trim();

        // Disallow whitespace and null
        while (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine("Please enter a valid first name");
            Console.Write("Enter first name: ");
            firstName = Console.ReadLine().Trim();
        }

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine().Trim();

        while (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("Please enter a valid last name");
            Console.Write("Enter last name: ");
            lastName = Console.ReadLine().Trim();
        }

        Console.Write("Enter email address: ");
        string email = Console.ReadLine().Trim();

        Console.Write("Enter phone number: ");
        string phone = Console.ReadLine().Trim();

        // Save contact
        Contact newContact = new(firstName, lastName, email, phone);
        contacts.Add(newContact);
        Console.WriteLine("Contact added successfully.");
    }

    public void SaveContactsToFile()
    {
        Console.Clear();
        // Do not continue if no contacts are saved
        if (!AreContactsStored())
            return;

        // Ask the user where it'd like to save the file
        // If the user doesn't answer, apply the default
        Console.Write("Enter the path to save the contacts file (default: contacts.txt in Downloads folder): ");
        string inputPath = Console.ReadLine().Trim();
        string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultSavePath : inputPath;

        try
        {
            // Ask before overwriting
            if (File.Exists(filePath))
            {
                Console.Write("File already exists. Do you want to overwrite? [Y/n]: ");
                if (Console.ReadLine() != "Y")
                    return;
            }

            /// Create a FileStream and write content
            FileStream outStream = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new(outStream);

            // Write out all contacts
            foreach (Contact contact in contacts)
            {
                writer.WriteLine(contact.FirstName);
                writer.WriteLine(contact.LastName);
                writer.WriteLine(contact.Email);
                writer.WriteLine(contact.Phone);
            }

            // Dispose writer and free memory
            writer.Dispose();

            // Confirm to user
            Console.WriteLine("Contacts saved to file successfully.");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }
        // Catch exceptions
        catch (FileNotFoundException)
        {
            Console.WriteLine("File {0} was not found", filePath);
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You do not have access to file {0}", filePath);
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

    public void LoadContactsFromFile()
    {
        Console.Clear();

        // Ask user which file to load
        Console.Write("Enter the path to load the contacts file (default: contacts.txt in Downloads folder): ");
        string inputPath = Console.ReadLine().Trim();
        string filePath = string.IsNullOrWhiteSpace(inputPath) ? defaultSavePath : inputPath;

        // Make sure the file exists before operation
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
            return;
        }

        contacts.Clear();
        using (StreamReader reader = new(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string firstName = line;
                string lastName = reader.ReadLine() ?? "";
                string email = reader.ReadLine() ?? "";
                string phone = reader.ReadLine() ?? "";
                Contact newContact = new(firstName, lastName, email, phone);
                contacts.Add(newContact);
            }
        }

        // Confirm to user
        Console.WriteLine("Contacts loaded from file successfully.");
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
    }

    public void ViewAllContactNames()
    {
        Console.Clear();
        // Do not continue if no contacts are saved
        if (!AreContactsStored())
            return;

        int i = 1;
        Console.WriteLine("All contacts:");

        // Loop through all contacts
        foreach (Contact contact in contacts)
        {
            Console.WriteLine("{0}: {1} {2}", i, contact.FirstName, contact.LastName);
            i++;
        }
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
    }

    public void ViewContactDetails()
    {
        Console.Clear();
        // Do not continue if no contacts are saved
        if (!AreContactsStored())
            return;

        // Ask which contact the user wants to see
        Console.Write("Enter the index of the contact: ");

        // Parse input and show contact details
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index < contacts.Count + 1)
        {
            Contact contact = contacts[index - 1];
            Console.WriteLine($"First name:\t{contact.FirstName}");
            Console.WriteLine($"Last name:\t{contact.LastName}");
            Console.WriteLine($"Email:\t{contact.Email}");
            Console.WriteLine($"Phone:\t{contact.Phone}");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }
        else
        {
            // Write error if user entered an invalid number
            Console.WriteLine("Invalid index.");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }
    }

    public void RemoveContact()
    {
        Console.Clear();
        // Do not continue if no contacts are saved
        if (!AreContactsStored())
            return;

        // Ask which contact to remove
        Console.WriteLine("Enter the index of contact to remove: ");

        // Parse input
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < contacts.Count)
        {
            // Remove the contact and verify to user
            contacts.RemoveAt(index);
            Console.WriteLine("Contact removed successfully!");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }
        else
        {
            // Write error if user enters invalid index
            Console.WriteLine("Invalid index");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
        }
    }
}

class Program
{
    static void Main()
    {
        // Create an instance to manage contacts with
        ContactManager contactManager = new();

        while (true)
        {
            // Display a menu
            Console.Clear();
            Console.WriteLine("1. Add contact");
            Console.WriteLine("2. Save contacts to file");
            Console.WriteLine("3. Load contacts from file");
            Console.WriteLine("4. View all contacts");
            Console.WriteLine("5. View contact details");
            Console.WriteLine("6. Remove contact");
            Console.WriteLine("7. Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    contactManager.AddContact();
                    break;
                case "2":
                    contactManager.SaveContactsToFile();
                    break;
                case "3":
                    contactManager.LoadContactsFromFile();
                    break;
                case "4":
                    contactManager.ViewAllContactNames();
                    break;
                case "5":
                    contactManager.ViewContactDetails();
                    break;
                case "6":
                    contactManager.RemoveContact();
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    break;
            }
        }
    }
}
