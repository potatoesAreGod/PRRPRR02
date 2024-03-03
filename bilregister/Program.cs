class Vehicle(string LicenseNumber, string VehicleType, int TopSpeed)
{
    // Class for the vehicles
    public string LicenseNumber { get; set; } = LicenseNumber;
    public string VehicleType { get; set; } = VehicleType;
    public int TopSpeed { get; set;} = TopSpeed;

    // Displays info for a vehicle when called
    public void ShowInformation()
    {
        Console.WriteLine("License number: {0}, vehicle type: {1}, top speed: {2}", LicenseNumber, VehicleType, TopSpeed);
    }
}

class VehicleRegistry
{
    // List to store registered vehicles
    private readonly List<Vehicle> vehicles;

    // Initialize list
    public VehicleRegistry() { vehicles = []; }

    // Method for adding vehicles
    public void Add()
    {
        Console.Write("Enter license plate number: ");
        string? plate = Console.ReadLine()?.Trim().ToUpper();

        // Disallow whitespace and null
        while (string.IsNullOrWhiteSpace(plate))
        {
            Console.WriteLine("Please enter a valid plate");
            Console.Write("Enter license plate: ");
            plate = Console.ReadLine();
        }

        // Check that the plate isn't already registered
        var vehicleToRegister = vehicles.FirstOrDefault(v => v.LicenseNumber == plate);
        if (vehicleToRegister != null)
        {
            Console.WriteLine("Error: {0} is already registered", plate);
            return;
        }

        Console.Write("Enter vechicle type (car or bike): ");
        string? type = Console.ReadLine()?.Trim().ToLower();

        // Make sure user enters either car or bike
        while (type != "car" && type != "bike")
        {
            Console.WriteLine("Enter either car or bike");
            Console.Write("Enter type: ");
            type = Console.ReadLine();
        }

        Console.Write("Enter vehicle top speed: ");
        int speed;

        // Make sure user enters a valid top speed
        while (!int.TryParse(Console.ReadLine()?.Trim(), out speed) || speed <= 0)
        {
            Console.WriteLine("Please enter a positive whole number for top speed");
            Console.Write("Enter top speed: ");
        }

        // Register vehicle
        Vehicle newVehicle = new(plate, type, speed);
        vehicles.Add(newVehicle);
        Console.WriteLine("{0} was added successfully!", plate);
    }
    public void Display()
    {
        // Exit if there's no vehicles registered
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No registered vehicles");
            return;
        }

        // Ask which vehicles the user wants to view
        Console.WriteLine("What vehicles do you want (car, bike, all): ");
        string? type = Console.ReadLine()?.Trim().ToLower();

        // Validate input
        while (type != "car" && type != "bike" && type != "all")
        {
            Console.WriteLine("Enter either car or bike");
            Console.Write("Enter type: ");
            type = Console.ReadLine()?.Trim().ToLower();
        }

        // Display vehicles based on user input
        if (type == "car" || type == "bike")
        {
            foreach (var vehicle in vehicles)
            {
                if (vehicle.VehicleType == type)
                    vehicle.ShowInformation();
            }
        }
        else
        {
            foreach (var vehicle in vehicles)
                vehicle.ShowInformation();
        }
    }

    // Method to remove vehicles
    public void Remove()
    {
        // Exit if there's no registed vehicles
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles registered");
            return;
        }

        Console.Write("Enter the licence plate number of the vehicle you want to remove: ");
        string? plate = Console.ReadLine()?.Trim().ToUpper();

        // Validate input
        while (string.IsNullOrWhiteSpace(plate))
        {
            Console.WriteLine("Please enter a valid plate");
            Console.Write("Enter license plate: ");
            plate = Console.ReadLine()?.Trim().ToUpper();
        }

        // Find and remove vehicle
        var vehicleToRemove = vehicles.FirstOrDefault(v => v.LicenseNumber == plate);
        if (vehicleToRemove != null)
        {
            vehicles.Remove(vehicleToRemove);
            Console.WriteLine("{0} was removed successfully", plate);
        }
        else
            Console.WriteLine("{0} was not found. Registry not modified", plate);
    }
}

class Program
{
    static void Main()
    {
        // Create a new registry to save vehicles in
        VehicleRegistry vehicleRegistry = new();

        while (true)
        { 
            // Display menu options
            Console.WriteLine("1. Add new vechicle");
            Console.WriteLine("2. Display registered vehicles");
            Console.WriteLine("3. Remove a vehicle");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            string res = Console.ReadLine()?.Trim() ?? "";

            switch (res)
            {
                case "1":
                    vehicleRegistry.Add();
                    break;

                case "2":
                    vehicleRegistry.Display();
                    break;

                case "3":
                    vehicleRegistry.Remove();
                    break;

                case "4":
                    Console.WriteLine("Bye o/");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Enter a number between 1 and 4");
                    break;
            }
        }
    }
}
